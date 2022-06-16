using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;

namespace AISandbox {
    public class Pathfinding : MonoBehaviour
    {
        public Grid grid;
        [SerializeField] private int columnSize;
        [SerializeField] private int rowSize;
        int minfValue = 0;
        KeyValuePair<int, GridNode> startNode, endNode, nullNode;
        KeyValuePair<int, GridNode> nullKPNode;
        public List<Vector3> BackTrace;
        public PriorityQueue<int, GridNode> OpenList;
        public PriorityQueue<int, GridNode> CLosedList;
        private int manhattanDistance = 0;
        private IList<KeyValuePair<int, GridNode>> GetNeighbours;
        IList<KeyValuePair<int, GridNode>> AllGridNodes;
        KeyValuePair<int, GridNode> currentNode;
        bool isUpdateOn;
        LineRenderer lr;
        public int lengthOfLineRenderer;


        private void Start()
        {
            lr = GetComponent<LineRenderer>();
            lr.startColor = Color.yellow;
            lr.endColor = Color.red;
            lr.startWidth = 0.3f;
            lr.endWidth = 0.3f;

            // Create and center the grid
            grid.Create(columnSize, rowSize);
            Vector2 gridSize = grid.size;
            Vector2 gridPos = new Vector2(gridSize.x * -0.5f, gridSize.y * 0.5f);
            grid.transform.position = gridPos;
            OpenList = new PriorityQueue<int, GridNode>();
            CLosedList = new PriorityQueue<int, GridNode>();
            lengthOfLineRenderer = 10;

            BackTrace = new List<Vector3>();
            isUpdateOn = true;
            
        }

        private void Update()
        {
            startNode = FindStartNode();
            endNode = FindEndNode();
            if(isUpdateOn)
            {
                if (startNode.Value != null && endNode.Value != null)
                {
                    if (startNode.Value.isSet == false)
                    {
                        currentNode = nullNode;
                        currentNode = startNode;
                        startNode.Value.isSet = true;
                       
                        startNode.Value.h_value = GetManhattanDistance(startNode, endNode);
                        startNode.Value.g_value = 10;
                        startNode.Value.f_value = startNode.Value.g_value + startNode.Value.h_value;
                        startNode.Value.ParentNode = null;
                        CalculateF_G_H_values(startNode);
                        CLosedList.Enqueue(startNode.Value.f_value, startNode.Value);
                    }
                    if (OpenList.IsEmpty == false)
                    {
                        currentNode = OpenList.Dequeue();      
                        if(currentNode.Value != endNode.Value)
                        CalculateF_G_H_values(currentNode);
                        NextNode(currentNode);
                    } 
                    else if(OpenList.IsEmpty == true)
                    {
                        isUpdateOn = false;
                        Debug.Log("No Path found");
                    }  
                    
                }
            }
            
        }

        private KeyValuePair<int, GridNode> FindStartNode()
        {
            AllGridNodes = grid.AllNodeList();
            foreach(KeyValuePair<int, GridNode> node in AllGridNodes)
            {
                if(node.Value.start_node == true)
                {                 
                    return node;
                }
            }
            return nullNode;            
        }

        private KeyValuePair<int, GridNode> FindEndNode()
        {
            AllGridNodes = grid.AllNodeList();
            foreach (KeyValuePair<int, GridNode> node in AllGridNodes)
            {
                if (node.Value.end_node == true)
                {                   
                    return node;
                }
            }
            return nullNode;
        }

        private void CalculateF_G_H_values(KeyValuePair<int, GridNode> currentNode)
        { 
           
            GetNeighbours = grid.GetNodeNeighbors(currentNode.Value.row, currentNode.Value.column);
            
            foreach(KeyValuePair<int, GridNode> node in GetNeighbours)
            {
                if (CLosedList.Contains(node) == false)
                {
                    if (OpenList.Contains(node) == false)
                    {
                        node.Value.ParentNode = currentNode.Value;
                        node.Value.g_value = CostSoFar(node.Value) + 10;
                        node.Value.h_value = GetManhattanDistance(node, endNode);
                        node.Value.f_value = node.Value.g_value + node.Value.h_value;
                        if (node.Value != endNode.Value)
                        {
                            node.Value.searchedColor = true;
                        }
                        

                        OpenList.Enqueue(node.Value.f_value, node.Value);
                    }
                    else if(OpenList.Contains(node)== true)
                    {
                        int tempG = node.Value.g_value;
                        int tempf = node.Value.f_value;
                        GridNode tempParent = node.Value.ParentNode;

                        node.Value.ParentNode = currentNode.Value;
                        node.Value.g_value = CostSoFar(node.Value) + 10;
                        node.Value.f_value = node.Value.g_value + node.Value.h_value;
                        if(node.Value.g_value > tempG)
                        {
                            node.Value.g_value = tempG;
                            node.Value.f_value = tempf;
                            node.Value.ParentNode = tempParent;
                        }


                    }
                    
                }
            }
        }

         private int CostSoFar(GridNode node)
        {
            int costSoFar = 0;
            while(node.ParentNode != null)
            {
                costSoFar += node.ParentNode.g_value;
                node = node.ParentNode;

            }

            return costSoFar;
        }

        private void BackTraceSearch(GridNode node)
        {
            int i = 0;
           
            while (node.ParentNode != null)
            {
               BackTrace.Add(node.transform.position);            
                node = node.ParentNode;
            }

            BackTrace.Add(startNode.Value.transform.position);
            lr.positionCount = BackTrace.Count;
           
            lr.SetPositions(BackTrace.ToArray());
        }
        void NextNode(KeyValuePair<int, GridNode> node)
        {
            if (node.Value == endNode.Value)
            {
                isUpdateOn = false;
                Debug.Log("EndNode found");
                BackTraceSearch(node.Value);

            }
            else if(node.Value == startNode.Value)
            {
                // do nothing
            }
            CLosedList.Enqueue(node.Value.f_value, node.Value);
        }

        private KeyValuePair<int, GridNode> NewNode(int minfvalue)
        {
            foreach (KeyValuePair<int, GridNode> node in GetNeighbours)
            {
                if (minfValue == node.Value.f_value)

                {
                    OpenList.Dequeue();
                    return node;

                }
            }

            return nullKPNode;
        }

        private int GetManhattanDistance(KeyValuePair<int, GridNode> firstNode, KeyValuePair<int, GridNode> secondNode)
        {
            manhattanDistance = 0;
            if (firstNode.Value != null && secondNode.Value != null)
            {
                manhattanDistance = (int)(Mathf.Abs(firstNode.Value.transform.position.x - secondNode.Value.transform.position.x) + Mathf.Abs(firstNode.Value.transform.position.y - secondNode.Value.transform.position.y));
                return manhattanDistance;
            }

            return 0;
        }

    }


   
}