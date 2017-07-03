using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace AISandbox
{

   
    public class GridNode : MonoBehaviour, IDropHandler
    {
        private SpriteRenderer _renderer;
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

        public Sprite KeySprite;
        public Sprite DoorSprite;
        public Sprite TreasureSprite;
        public Sprite SquareSprite;
        public enum TerrainType { Road = 1, Forest, Water }
        [SerializeField]
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

        public enum GridNodeType { RedKey, GreenKey, BlueKey, RedDoor, GreenDoor, BlueDoor, Treasure, Normal }
        [SerializeField]
        private GridNodeType _gridNodeType = GridNodeType.Normal;
        public GridNodeType gridNodeType
        {
            get
            {
                return _gridNodeType;
            }
            set
            {
                _gridNodeType = value;
                switch (_gridNodeType)
                {
                    case GridNodeType.Normal:
                        blocked = false;
                        break;
                    case GridNodeType.RedDoor:
                    case GridNodeType.GreenDoor:
                    case GridNodeType.BlueDoor:
                        blocked = true;
                        break;
                    default:
                        break;
                }
            }
        }

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
                if (_blocked)
                {
                    _renderer.color = CreatingGridAndActor._blockedColor;
                    
                }
                else
                {
                    _renderer.sprite = SquareSprite;
                    if (_terrainType == TerrainType.Forest)
                    {
                        //_renderer.sprite = CreatingGridAndActor.forest;
                        _renderer.color = CreatingGridAndActor._forestColor;
                    }
                    else if (_terrainType == TerrainType.Road)
                    {
                        //_renderer.sprite = CreatingGridAndActor.road;
                        _renderer.color = CreatingGridAndActor._roadColor;
                    }
                    else
                    {
                        //_renderer.sprite = CreatingGridAndActor.water;
                        _renderer.color = CreatingGridAndActor._waterColor;
                    }
                }
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


        public bool KeyNode
        {
           
            set
            {
                
                _renderer.sprite = KeySprite;
            }
        }







        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            KeySprite = Resources.Load<Sprite>("Sprites/key");
            DoorSprite = Resources.Load<Sprite>("Sprites/door_close");
            TreasureSprite = Resources.Load<Sprite>("Sprites/treasure");
            SquareSprite = Resources.Load<Sprite>("Sprites/square");


        }

        public IList<KeyValuePair<int, GridNode>> GetNeighbors(int row, int column, bool include_diagonal = false)
        {
            IList<KeyValuePair<int, GridNode>> neighbors = new List<KeyValuePair<int, GridNode>>();
            int start_row = Mathf.Max(row - 1, 0);
            int start_col = Mathf.Max(column - 1, 0);
            int end_row = Mathf.Min(row + 1, CreatingGridAndActor.grid.nodes.GetLength(0) - 1);
            int end_col = Mathf.Min(column + 1, CreatingGridAndActor.grid.nodes.GetLength(1) - 1);

            for (int row_index = start_row; row_index <= end_row; ++row_index)
            {
                for (int col_index = start_col; col_index <= end_col; ++col_index)
                {
                    if (include_diagonal || row_index == row || col_index == column)
                    {
                        if (this.row != row_index || this.column != col_index)
                        {
                            neighbors.Add(new KeyValuePair<int, GridNode>(CreatingGridAndActor.grid.nodes[row_index, col_index].f, CreatingGridAndActor.grid.nodes[row_index, col_index]));
                        }
                    }
                }
            }
            return neighbors;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (EntryPoint.stateMachine.GetActiveStateName() == "PlacingGameObjects")
            {
                if (!blocked && gridNodeType == GridNodeType.Normal)
                {
                    gridNodeType = DragHandler.ItemType;
                    _renderer.sprite = DragHandler.sprite;
                    _renderer.color = DragHandler.color;                   
                    DragHandler.isDragging = false;
                   
                    DragHandler.dragHandlerScript.enabled = false;
                    DragHandler.item.SetActive(false);
                }
            }
        }
    }
}