using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;
using System;

namespace AISandbox
{
    public class Grid : MonoBehaviour
    {
        public GridNode gridNodePrefab;

        private GridNode[,] _nodes;
        private float _node_width;
        private float _node_height;
        public bool _draw_blocked;
        public bool draw_start;
        public bool draw_end;
        private Pathfinding pathF;
       
        public RectTransform container;
        private GameObject canvas;
        private GameObject reset;
        private Button startNodeButton;
        private Button endNodeButton;
        private Button blockedNodeButton;
        private Button resetButton;
        

        private bool isOpen;
        public bool is_Open
        {
            get
            {
                return isOpen;
            }
            set
            {
                isOpen = value;
            }
        }
        private GridNode CreateNode(int row, int col)
        {
            GridNode node = Instantiate<GridNode>(gridNodePrefab);
            node.name = string.Format("Node {0}{1}", (char)('A' + row), col);
            node.grid = this;
            node.row = row;
            node.column = col;
            node.transform.parent = transform;
            node.gameObject.SetActive(true);
            return node;
        }

        public void Create(int rows, int columns)
        {
            _node_width = gridNodePrefab.GetComponent<Renderer>().bounds.size.x;
            _node_height = gridNodePrefab.GetComponent<Renderer>().bounds.size.y;
            Vector2 node_position = new Vector2(_node_width * 0.5f, _node_height * -0.5f);
            _nodes = new GridNode[rows, columns];
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < columns; ++col)
                {
                    GridNode node = CreateNode(row, col);
                    node.transform.localPosition = node_position;
                    _nodes[row, col] = node;

                    node_position.x += _node_width;
                }
                node_position.x = _node_width * 0.5f;
                node_position.y -= _node_height;
            }
        }

        public Vector2 size
        {
            get
            {
                return new Vector2(_node_width * _nodes.GetLength(1), _node_height * _nodes.GetLength(0));
            }
        }

        public GridNode GetNode(int row, int col)
        {
            return _nodes[row, col];
        }


        public IList<KeyValuePair<int, GridNode>> GetNodeNeighbors(int row, int col, bool include_diagonal = false)
        {
            IList<KeyValuePair<int, GridNode>> neighbors = new List<KeyValuePair<int, GridNode>>();

            int start_row = Mathf.Max(row - 1, 0);
            int start_col = Mathf.Max(col - 1, 0);
            int end_row = Mathf.Min(row + 1, _nodes.GetLength(0) - 1);
            int end_col = Mathf.Min(col + 1, _nodes.GetLength(1) - 1);

            for (int row_index = start_row; row_index <= end_row; ++row_index)
            {
                for (int col_index = start_col; col_index <= end_col; ++col_index)
                {
                    if (include_diagonal || row_index == row || col_index == col)
                    {
                        if (_nodes[row, col].row == row_index && _nodes[row, col].column == col_index)
                        {

                        }
                        else
                        {
                           // _nodes[row_index, col_index].ParentNode = _nodes[row, col];
                            if(!_nodes[row_index, col_index].isBlocked)
                            { 
                            neighbors.Add(new KeyValuePair<int, GridNode>(_nodes[row_index, col_index].f_value, _nodes[row_index, col_index]));
                            }
                        }
                    }
                }
            }
            return neighbors;
        }


        public IList<KeyValuePair<int, GridNode>> AllNodeList()
        {
            IList<KeyValuePair<int, GridNode>> allNodes = new List<KeyValuePair<int, GridNode>>();
            foreach( GridNode node in _nodes)
            {
                KeyValuePair<int, GridNode> temp = new KeyValuePair<int, GridNode>(node.f_value,node);
                
                allNodes.Add(temp);
            }


            return allNodes;
        }

        private void Start()
        {
            canvas = GameObject.Find("Container");
            reset = GameObject.Find("Reset");
            container = canvas.transform.GetComponent<RectTransform>();

            startNodeButton = container.GetChild(0).GetComponent<Button>();
            endNodeButton = container.GetChild(1).GetComponent<Button>();
            blockedNodeButton = container.GetChild(2).GetComponent<Button>();
            resetButton = reset.GetComponent<Button>();
            startNodeButton.onClick.AddListener(OnClickStart);
            endNodeButton.onClick.AddListener(OnClickEnd);
            blockedNodeButton.onClick.AddListener(OnClickBlock);
            resetButton.onClick.AddListener(OnCLickReset);
            isOpen = false;
            _draw_blocked = false;
            draw_start = false;
            draw_end = false;

            pathF = GameObject.Find("Pathfinding").GetComponent<Pathfinding>();
        }

        private void Update()
        {

            Vector3 scale = container.localScale;
            scale.y = Mathf.Lerp(scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
            container.localScale = scale;

         

            if (Input.GetMouseButton(0))
            {
                Vector3 world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 local_pos = transform.InverseTransformPoint(world_pos);
                // This trick makes a lot of assumptions that the nodes haven't been modified since initialization.
                int column = Mathf.FloorToInt(local_pos.x / _node_width);
                int row = Mathf.FloorToInt(-local_pos.y / _node_height);
                if (row >= 0 && row < _nodes.GetLength(0)
                 && column >= 0 && column < _nodes.GetLength(1))
                {
                    GridNode node = _nodes[row, column];
                    if (Input.GetMouseButtonDown(0) && _draw_blocked == true)
                    {
                        _draw_blocked = !node.blocked;
                    }
                 
                    if (node.blocked != _draw_blocked)
                    {
                        node.blocked = _draw_blocked;

                    }
                   if (Input.GetMouseButtonDown(0) && draw_start == true)
                    {
                        draw_start = !node.startnode;
                        
                    }
                    if (node.startnode != draw_start)
                    {
                        node.startnode = draw_start;

                    }
                   if (Input.GetMouseButtonDown(0) && draw_end == true)
                    {
                        draw_end = !node.endnode;
                    }
                    if (node.endnode != draw_end)
                    {
                        node.endnode = draw_end;

                    }
                }
            }
        }



        public void OnClickStart()
        {
            
            draw_start = true;
            _draw_blocked = false;
            draw_end = false;
            isOpen = false;
            
        }

        public void OnClickEnd()
        {
            draw_end = true;
            _draw_blocked = false;
            draw_start = false;
            isOpen = false;
        }

        public void OnClickBlock()
        {
            _draw_blocked = true;
            draw_start = false;
            draw_end = false;
            isOpen = false;
        }

        public void OnCLickReset()
        {
            //SceneManager.LoadScene("StateMachines");
        }








    }
}