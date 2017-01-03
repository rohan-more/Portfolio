using UnityEngine;
using System.Collections.Generic;

namespace AISandbox
{
    public class GridNode : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Sprite blockedSprite;
        [HideInInspector]
        public Sprite unblockedSprite;
        private Color _blockedColor;
        public SpriteRenderer myRenderer
        {
            get
            {
                return _renderer;
            }
            set
            {
                _renderer = value;
            }
        }

        public enum TerrainType { Road = 1, Forest, Water }

        private TerrainType _terrainType = TerrainType.Road;
        public TerrainType terrainType
        {
            get
            {
                return _terrainType;
            }
            set
            {
                _terrainType = value;
            }
        }

        [HideInInspector]
        public Grid grid;

        [SerializeField]
        private GridNode _parentNode = null;
        public GridNode parentNode
        {
            get
            {
                return _parentNode;
            }
            set
            {
                _parentNode = value;
            }
        }
        [SerializeField]
        private int _column;
        public int column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }
        [SerializeField]
        private int _row;
        public int row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }
        [SerializeField]
        private bool _blocked = false;
        public bool blocked
        {
            get
            {
                return _blocked;
            }
            set
            {
                _blocked = value;
               
                _renderer.color = _blockedColor;
                
            }
        }
        [SerializeField]
        private bool _tempBlocked = false;
        public bool tempBlocked
        {
            get
            {
                return _tempBlocked;
            }
            set
            {
                _tempBlocked = value;
            }
        }
        [SerializeField]
        private bool _isStartNode = false;
        public bool isStartNode
        {
            get
            {
                return _isStartNode;
            }
            set
            {
                _isStartNode = value;
            }
        }
        [SerializeField]
        private bool _isEndNode = false;
        public bool isEndNode
        {
            get
            {
                return _isEndNode;
            }
            set
            {
                _isEndNode = value;
            }
        }
        [SerializeField]
        private int _f = 0;
        public int f
        {
            get
            {
                return _f;
            }
            set
            {
                _f = value;
            }
        }
        [SerializeField]
        private int _g = 0;
        public int g
        {
            get
            {
                return _g;
            }
            set
            {
                _g = value;
            }
        }
        [SerializeField]
        private int _h = 0;
        public int h
        {
            get
            {
                return _h;
            }
            set
            {
                _h = value;
            }
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
           // blockedSprite = Resources.Load<Sprite>("Sprites/blocked");
            _blockedColor = new Color(0.647f, 0.164f, 0.164f);
        }

        public IList<KeyValuePair<int,GridNode>> GetNeighbors(int row, int column, bool include_diagonal = false)
        {
            IList<KeyValuePair<int, GridNode>> neighbors = new List<KeyValuePair<int, GridNode>>();
            int start_row = Mathf.Max(row - 1, 0);
            int start_col = Mathf.Max(column - 1, 0);
            int end_row = Mathf.Min(row + 1, grid.nodes.GetLength(0) - 1);
            int end_col = Mathf.Min(column + 1, grid.nodes.GetLength(1) - 1);

            for (int row_index = start_row; row_index <= end_row; ++row_index)
            {
                for (int col_index = start_col; col_index <= end_col; ++col_index)
                {
                    if (include_diagonal || row_index == row || col_index == column)
                    {
                        if (this.row == row_index && this.column == col_index)
                        {

                        }
                        else
                        {
                            neighbors.Add(new KeyValuePair<int, GridNode>(grid.nodes[row_index, col_index].f, grid.nodes[row_index, col_index]));
                        }                        
                    }
                }
            }
            return neighbors;
        }
    }
}