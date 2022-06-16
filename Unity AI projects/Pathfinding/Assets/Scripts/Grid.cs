using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AISandbox
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private GridNode gridNodePrefab;
        public GridNode[,] _nodes;
        [SerializeField] private const float _node_width = 1.92f;
        [SerializeField] private const float _node_height = 1.92f;
        public bool _draw_blocked;
        public bool draw_start;
        public bool draw_end;

        private void Start()
        {
            _draw_blocked = false;
            draw_start = false;
            draw_end = false;
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


    }
}