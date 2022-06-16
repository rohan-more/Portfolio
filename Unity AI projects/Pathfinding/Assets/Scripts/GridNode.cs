using UnityEngine;
using System.Collections.Generic;

namespace AISandbox {
    public class GridNode : MonoBehaviour {
        public Grid grid;
        public int column;
        public int row;

        private SpriteRenderer _renderer;
        private Color _orig_color;
        private Color _blocked_color;
        private Color _searchedColor;
        private Color _pathColor;
        private Color start_color;
        private Color end_color;
        public int f_value =0;
        public int g_value =0;
        public int h_value =0;
        public GridNode ParentNode;
        public bool isSet = false;
        [SerializeField]
        public bool isBlocked = false;
        private bool isSearched = false;
        private bool isPath = false;
        public bool start_node = false;
        public bool end_node = false;
        public bool blocked {
            get {
                return isBlocked;
            }
            set {
                isBlocked = value;
                _renderer.color = isBlocked ? _blocked_color : _orig_color;
            }
        }

        public bool pathColor
        {
            get
            {
                return isPath;
            }
            set
            {
                isPath = value;
                _renderer.color = isPath ? _pathColor : _orig_color;
            }
        }

        public bool searchedColor
        {
            get
            {
                return isSearched;
            }
            set
            {
                isSearched = value;
                _renderer.color = isSearched ? _searchedColor : _orig_color;
            }
        }
        public bool startnode
        {
            get
            {
                return start_node;
            }
            set
            {
                start_node = value;
                _renderer.color = start_node ? start_color : _orig_color;
            }
        }
        public bool endnode
        {
            get
            {
                return end_node;
            }
            set
            {
                end_node = value;
                _renderer.color = end_node ? end_color : _orig_color;
            }
        }

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            _orig_color = _renderer.color;
            _blocked_color = new Color( _orig_color.r * 0.5f, _orig_color.g * 0.5f, _orig_color.b * 0.5f );
            _searchedColor = Color.blue;
            _pathColor = Color.black;
            start_color = new Color(_orig_color.r * 0, _orig_color.g * 255.0f, _orig_color.b * 0);
            end_color = new Color(_orig_color.r * 255.0f, _orig_color.g * 0, _orig_color.b * 0);
            isSet = false;
        }
        public IList<KeyValuePair<int, GridNode>>GetNeighbors( bool include_diagonal = false ) {
            return grid.GetNodeNeighbors( row, column, include_diagonal );
        }
    }
}