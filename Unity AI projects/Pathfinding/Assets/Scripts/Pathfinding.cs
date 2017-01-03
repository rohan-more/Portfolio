using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;

namespace AISandbox {
    public class Pathfinding : MonoBehaviour
    {
        public Grid grid;
       // GridNode grid_node;
        int columnSize = 15;
        int rowSize = 15;
        int minfValue = 0;
        /// <typeparam name="TPriority">Type of priorities</typeparam>
        /// <typeparam name="TValue">Type of values</typeparam>
        KeyValuePair<int, GridNode> startNode, endNode, nullNode;
        KeyValuePair<int, GridNode> nullKPNode;
        public List<Vector3> BackTrace;
        public PriorityQueue<int, GridNode> OpenList;
        public PriorityQueue<int, GridNode> CLosedList;
        public int MANHATTANDISTANCE = 0;
        private IList<KeyValuePair<int, GridNode>> GetNeighbours;
        IList<KeyValuePair<int, GridNode>> AllGridNodes;
        KeyValuePair<int, GridNode> currentNode;
        bool isUpdateOn;
        LineRenderer lr;
        private Color c1 = Color.yellow;
        private Color c2 = Color.red;
        public int lengthOfLineRenderer;


        private void Start()
        {
            lr = GetComponent<LineRenderer>();
            lr.SetColors(c1, c2);
            lr.SetWidth(0.3f,0.3f);
            //lr.SetVertexCount(BackTrace.Count);
            // Create and center the grid
            grid.Create(columnSize, rowSize);
            Vector2 gridSize = grid.size;
            Vector2 gridPos = new Vector2(gridSize.x * -0.5f, gridSize.y * 0.5f);
            grid.transform.position = gridPos;
           // grid_node = GameObject.Find("GridNodePrefab").GetComponent<GridNode>();
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
           // if (Input.GetKeyDown(KeyCode.Space))
            if(isUpdateOn)
            {
                //ManhattanDistance(startNode,endNode);
                if (startNode.Value != null && endNode.Value != null)
                {
                    if (startNode.Value.isSet == false)
                    {
                        currentNode = nullNode;
                        currentNode = startNode;
                        startNode.Value.isSet = true;
                       
                        startNode.Value.h_value = ManhattanDistance(startNode, endNode);
                        startNode.Value.g_value = 10;
                        startNode.Value.f_value = startNode.Value.g_value + startNode.Value.h_value;
                        startNode.Value.ParentNode = null;
                        CalculateF_G_H_values(startNode);
                        CLosedList.Enqueue(startNode.Value.f_value, startNode.Value);
                    }
                    if (OpenList.IsEmpty == false)
                    {
                        currentNode = OpenList.Dequeue();

                       // if (CLosedList.Contains(currentNode.f_value,cur) == false && OpenList.Contains(node) == false)
                           
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
                        node.Value.h_value = ManhattanDistance(node, endNode);
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

            int a = 0;

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
                //BackTrace[i] = new Vector3(node.ParentNode.transform.position.x,node.ParentNode.transform.position.y, node.ParentNode.transform.position.z);
                node = node.ParentNode;
                //i++;

            }
            //lengthOfLineRenderer = BackTrace.Length;
            BackTrace.Add(startNode.Value.transform.position);
            lr.SetVertexCount(BackTrace.Count);
           
            lr.SetPositions(BackTrace.ToArray());
        }
        void NextNode(KeyValuePair<int, GridNode> node)
        {
            int a = 0;
           // AddToOpenList = OpenList.Dequeue();
            
            
            Debug.Log(node.Value);
            if (node.Value == endNode.Value)
            {
                isUpdateOn = false;
                Debug.Log("EndNode found");
                BackTraceSearch(node.Value);

            }
            else if(node.Value == startNode.Value)
            {

            }
            else
            {
                //node.Value.pathColor = true;
            }
            CLosedList.Enqueue(node.Value.f_value, node.Value);
            int b = 0;
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

        private int ManhattanDistance(KeyValuePair<int, GridNode> firstNode, KeyValuePair<int, GridNode> secondNode)
        {
            MANHATTANDISTANCE = 0;
            if (firstNode.Value != null && secondNode.Value != null)
            {
                MANHATTANDISTANCE = (int)(10 * (Mathf.Abs(firstNode.Value.transform.position.x - secondNode.Value.transform.position.x) + Mathf.Abs(firstNode.Value.transform.position.y - secondNode.Value.transform.position.y)));
                return MANHATTANDISTANCE;
            }

            return 0;
        }

    }


   
}