using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AISandbox
{
    public class Grid : MonoBehaviour
    {
        private GridNode gridNodePrefab;
        private Sprite road;
        private Sprite forest;
        private Sprite water;
        private SpriteRenderer _renderer;
        private Color _waterColor;
        private Color _forestColor;
        private Color _roadColor;
        private Pathfollowing pathfollowingScript;

        private GridNode[,] _nodes;
        public GridNode[,] nodes
        {
            get
            {
                return _nodes;
            }
        }

        private float _node_width;
        private float _node_height;
        private bool _draw_blocked;

        private GridNode CreateNode(int row, int col)
        {
            GridNode node = Instantiate(gridNodePrefab, transform) as GridNode;
            node.name = string.Format("Node {0}{1}", (char)('A' + row), col);
            node.tag = "Node";
            node.grid = this;
            node.row = row;
            node.column = col;
            node.terrainType = (GridNode.TerrainType)Random.Range(1, 4);
            if (node.terrainType == GridNode.TerrainType.Road)
            {
             
                node.myRenderer.color = _roadColor;
            }
            else if (node.terrainType == GridNode.TerrainType.Forest)
            {
              
                node.myRenderer.color =  _forestColor;
            }
            else
            {
                node.myRenderer.color = _waterColor;
            }
            return node;
        }

        private void Awake()
        {
            gridNodePrefab = Resources.Load<GridNode>("Prefabs/GridNodePrefab");
        
            pathfollowingScript = GameObject.Find("Pathfollowing").GetComponent<Pathfollowing>();
            _waterColor = new Color(0.0f, 0.0f, 255.0f);
            _forestColor = new Color(0.0f, 128.0f, 0.0f);
            _roadColor = new Color(0.501f, 0.501f, 0.501f);
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

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 local_pos = transform.InverseTransformPoint(world_pos);
                // This trick makes a lot of assumptions that the nodes haven't been modified since initialization.
                int column = Mathf.FloorToInt(local_pos.x / _node_width);
                int row = Mathf.FloorToInt(-local_pos.y / _node_height);
                if (row >= 0 && row < _nodes.GetLength(0) && column >= 0 && column < _nodes.GetLength(1))
                {
                    GridNode node = _nodes[row, column];
                    if (pathfollowingScript.gamestate == Pathfollowing.Gamestates.DrawingWalls)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            _draw_blocked = !node.blocked;
                        }
                        if (node.blocked != _draw_blocked)
                        {
                            node.blocked = _draw_blocked;
                        }
                    }                    
                }
            }
        }
    }
}