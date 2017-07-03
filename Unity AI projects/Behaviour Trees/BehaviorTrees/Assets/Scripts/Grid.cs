using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AISandbox
{
    public class Grid : MonoBehaviour
    {
        private GridNode _gridNodePrefab;
        public GridNode gridNodePrefab
        {
            get
            {
                return _gridNodePrefab;
            }
        }

        private GridNode[,] _nodes;
        public GridNode[,] nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                _nodes = value;
            }
        }

        private float _node_width;
        public float node_width
        {
            get
            {
                return _node_width;
            }
        }
        private float _node_height;
        public float node_height
        {
            get
            {
                return _node_height;
            }
        }
        private Dictionary<GridNode.GridNodeType, GridNode> _specialNodes;
        public Dictionary<GridNode.GridNodeType, GridNode> specialNodes
        {
            get
            {
                return _specialNodes;
            }
        }
        private KeyValuePair<GridNode.GridNodeType, GridNode> _specialNode;
        public KeyValuePair<GridNode.GridNodeType, GridNode> specialNode
        {
            set
            {
                _specialNode = value;
                specialNodes[_specialNode.Key] = _specialNode.Value;
            }
        }

        private void Awake()
        {
            _specialNodes = new Dictionary<GridNode.GridNodeType, GridNode>();
            _gridNodePrefab = Resources.Load<GridNode>("Prefabs/GridNodePrefab");
            _node_width = _gridNodePrefab.GetComponent<Renderer>().bounds.size.x;
            _node_height = _gridNodePrefab.GetComponent<Renderer>().bounds.size.y;
        }

        public Vector2 size
        {
            get
            {
                return new Vector2(_node_width * _nodes.GetLength(1), _node_height * _nodes.GetLength(0));
            }
        }      
    }
}