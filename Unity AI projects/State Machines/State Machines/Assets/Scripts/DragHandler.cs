using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AISandbox
{
    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GridNode.GridNodeType itemType;
        public static GridNode.GridNodeType ItemType = GridNode.GridNodeType.Normal;
        private static bool _isDragging = false;
        public static bool isDragging
        {
            get
            {
                return _isDragging;
            }
            set
            {
                _isDragging = value;
            }
        }
        private Vector3 _startPosition;
        public Vector3 startPosition
        {
            get
            {
                return _startPosition;
            }
        }
        private CanvasGroup _canvasGroup;
        public CanvasGroup canvasGroup
        {
            get
            {
                return _canvasGroup;
            }
        }
        private static Sprite _sprite = null;
        public static Sprite sprite
        {
            get
            {
                return _sprite;
            }
        }
        private static Color _color = Color.clear;
        public static Color color
        {
            get
            {
                return _color;
            }
        }
        private static string _tagName = null;
        public static string tagName
        {
            get
            {
                return _tagName;
            }
        }
        private static GameObject _item = null;
        public static GameObject item
        {
            get
            {
                return _item;
            }
        }
        private static DragHandler _dragHandlerScript = null;
        public static DragHandler dragHandlerScript
        {
            get
            {
                return _dragHandlerScript;
            }
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (EntryPoint.stateMachine.GetActiveStateName() == "PlacingGameObjects")
            {
                _isDragging = true;
                _startPosition = transform.position;
                _sprite = GetComponent<Image>().sprite;
                _color = GetComponent<Image>().color;
                _tagName = gameObject.tag;
                _item = gameObject;
                _dragHandlerScript = this;
                ItemType = itemType;
                _canvasGroup.blocksRaycasts = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (EntryPoint.stateMachine.GetActiveStateName() == "PlacingGameObjects")
            {
                _isDragging = true;
                transform.position = Input.mousePosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (EntryPoint.stateMachine.GetActiveStateName() == "PlacingGameObjects")
            {
                _isDragging = false;
                _sprite = null;
                _color = Color.clear;
                _tagName = null;
                _item = null;
                _dragHandlerScript = null;
                _canvasGroup.blocksRaycasts = true;
                transform.position = _startPosition;
            }
        }
    }
}

