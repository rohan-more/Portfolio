using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AISandbox
{
    public class Pathfinding : MonoBehaviour
    {
        private const int STRAIGHTMANHATTENCOST = 10;
        private const int DIAGONALMANHATTENCOST = 14;

        [HideInInspector]
        public PriorityQueue<int, GridNode> openList;
        [HideInInspector]
        public PriorityQueue<int, GridNode> closedList;
       
        public bool pathNotFound = true;
        [HideInInspector]
        public bool newStartNodeAssigned = false;
        public GridNode startNode;
        public GridNode endNode;
        public GridNode goalNode;
        
        private Pathfollowing pathfollowingScript;

        private void Awake()
        {
            openList = new PriorityQueue<int, GridNode>();
            closedList = new PriorityQueue<int, GridNode>();
            pathfollowingScript = GameObject.Find("Pathfollowing").GetComponent<Pathfollowing>();
            
        }

        public void Recalculate_AStar()
        {
            if (pathfollowingScript.gamestate == Pathfollowing.Gamestates.PathFinding)
            {
                if ((openList.Count == 1) && closedList.IsEmpty)
                {
                    while (pathNotFound)
                    {
                        if (!openList.IsEmpty)
                        {
                            KeyValuePair<int, GridNode> currentNode = openList.Dequeue();
                            IList<KeyValuePair<int, GridNode>> neighbors = currentNode.Value.GetNeighbors(currentNode.Value.row, currentNode.Value.column, true);
                            ProcessNeighbors(neighbors, currentNode);
                            foreach (KeyValuePair<int, GridNode> neighbor in neighbors)
                            {
                                neighbor.Value.tempBlocked = false;
                            }
                            closedList.Enqueue(currentNode.Value.f, currentNode.Value);
                            if (currentNode.Value == endNode)
                            {
                                pathNotFound = false;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        public void AssignStartEndNode(GridNode startNode, GridNode endNode)
        {
            this.startNode = startNode;
            this.startNode.parentNode = null;
            this.startNode.g = 0;
            this.startNode.h = 0;
            this.startNode.f = this.startNode.g + this.startNode.h;
            openList.Enqueue(this.startNode.f, this.startNode);
            this.endNode = endNode;         
        }

       
        private void ProcessNeighbors(IList<KeyValuePair<int, GridNode>> neighbors, KeyValuePair<int, GridNode> currentNode)
        {
            foreach (KeyValuePair<int, GridNode> neighbor in neighbors)
            {
                if (!CheckDiagonalNeighbor(currentNode.Value, neighbor.Value))
                {
                    AvoidCornerCutting(currentNode.Value, neighbor.Value, ref neighbors);
                }
            }
            foreach (KeyValuePair<int, GridNode> neighbor in neighbors)
            {
                if (!neighbor.Value.blocked && !neighbor.Value.tempBlocked && !closedList.Contains(neighbor))
                {
                    bool isDiagonal = CheckDiagonalNeighbor(currentNode.Value, neighbor.Value);
                    if (isDiagonal)
                    {
                        if (NoDiagonalCut(neighbor.Value))
                        {
                            continue;
                        }
                    }
                    if (!openList.Contains(neighbor))
                    {
                        neighbor.Value.parentNode = currentNode.Value;
                        neighbor.Value.g = (int)neighbor.Value.terrainType *
                            (isDiagonal ? DIAGONALMANHATTENCOST : STRAIGHTMANHATTENCOST) + CostSoFar(neighbor.Value);
                        neighbor.Value.h = CalculateHValue(neighbor.Value);
                        neighbor.Value.f = neighbor.Value.g + neighbor.Value.h;
                        openList.Enqueue(neighbor.Value.f, neighbor.Value);
                    }
                    else
                    {
                        openList.Remove(neighbor);
                        GridNode tempParentNode = neighbor.Value.parentNode;
                        int tempG = neighbor.Value.g;
                        int tempF = neighbor.Value.f;
                        neighbor.Value.parentNode = currentNode.Value;
                        neighbor.Value.g = (int)neighbor.Value.terrainType *
                            (isDiagonal ? DIAGONALMANHATTENCOST : STRAIGHTMANHATTENCOST) + CostSoFar(neighbor.Value);
                        neighbor.Value.f = neighbor.Value.g + neighbor.Value.h;
                        if (neighbor.Value.g < tempG)
                        {
                            openList.Enqueue(neighbor.Value.f, neighbor.Value);
                        }
                        else
                        {
                            neighbor.Value.parentNode = tempParentNode;
                            neighbor.Value.g = tempG;
                            neighbor.Value.f = tempF;
                            openList.Enqueue(neighbor.Value.f, neighbor.Value);
                        }
                    }
                }
            }
        }

        private bool CheckDiagonalNeighbor(GridNode currentNode, GridNode neighbor)
        {
            return (currentNode.row != neighbor.row && currentNode.column != neighbor.column);
        }
        private int CalculateHValue(GridNode neighbor)
        {
            int hValue;
            float xGap = Mathf.Abs(neighbor.transform.position.x - endNode.transform.position.x);
            float yGap = Mathf.Abs(neighbor.transform.position.y - endNode.transform.position.y);
            if (xGap > yGap)
            {
                hValue = (int)(((int)neighbor.terrainType * DIAGONALMANHATTENCOST * xGap) + ((int)neighbor.terrainType * STRAIGHTMANHATTENCOST * (xGap - yGap)));
            }
            else
            {
                hValue = (int)(((int)neighbor.terrainType * DIAGONALMANHATTENCOST * xGap) + ((int)neighbor.terrainType * STRAIGHTMANHATTENCOST * (yGap - xGap)));
            }
            return hValue;
        }

     

        private bool NoDiagonalCut(GridNode neighbor)
        {
            bool result = true;
            IList<KeyValuePair<int, GridNode>> neighborsOfDiagonal = neighbor.GetNeighbors(neighbor.row, neighbor.column, false);
            foreach (KeyValuePair<int, GridNode> neighborOfDiagonal in neighborsOfDiagonal)
            {
                if (neighborOfDiagonal.Value.row > neighbor.row ||
                     neighborOfDiagonal.Value.column > neighbor.column)
                {
                    continue;
                }
                else
                {
                    result = result && neighborOfDiagonal.Value.blocked;
                }
            }
            return result;
        }

        private void AvoidCornerCutting(GridNode currentNode, GridNode currentNeighbor, ref IList<KeyValuePair<int, GridNode>> neighbors)
        {
            if (currentNeighbor.blocked)
            {
                if (currentNode.row == currentNeighbor.row)
                {
                    foreach (KeyValuePair<int, GridNode> neighbor in neighbors)
                    {
                        if (neighbor.Value.column == currentNeighbor.column)
                        {
                            neighbor.Value.tempBlocked = true;
                        }
                    }
                }
                if (currentNode.column == currentNeighbor.column)
                {
                    foreach (KeyValuePair<int, GridNode> neighbor in neighbors)
                    {
                        if (neighbor.Value.row == currentNeighbor.row)
                        {
                            neighbor.Value.tempBlocked = true;
                        }
                    }
                }
            }
        }

        private int CostSoFar(GridNode node)
        {
            int costSoFar = 0;
            while (node.parentNode != null)
            {
                costSoFar += node.parentNode.g;
                node = node.parentNode;
            }
            return costSoFar;
        }
    }
}