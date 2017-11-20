using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    // Use this for initialization
    GameObject _content;
    const float PREFERRED_HEIGHT = 111.93f;
    const float PREFERRED_WIDTH = 214.87f;

    //GameObject linksLibrary_Dummy;
    GameObject dummy;
    public bool selected;
    GameObject canvas;
    Image thisImage;
     Transform originalParent;
    string _name;
    public bool IsDragable;
    public int siblingIndex;
    public int IdleValue;
    public int AttackValue;
    public int DodgeValue;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        _content = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("ChainInspector").transform.Find("Viewport").transform.Find("Content").gameObject;
        originalParent = transform.parent;
        _name = gameObject.name;
       IdleValue = 0;
       AttackValue=  0;
       DodgeValue= 0;
 
       thisImage = GetComponent<Image>();
        IsDragable = true;

    }


    public void OnBeginDrag(PointerEventData eventdata)
    {
        if(IsDragable)
        {
            transform.SetParent(canvas.transform);
            LayoutElement newLinkLayout = GetComponent<LayoutElement>();
            newLinkLayout.enabled = true;
            newLinkLayout.preferredHeight = PREFERRED_HEIGHT;
            newLinkLayout.preferredWidth = PREFERRED_WIDTH;
            newLinkLayout.flexibleHeight = 0;
            newLinkLayout.flexibleWidth = 0;
            if(CanvasManager.Instance.transform.childCount > 0)
            {
                CanvasManager.Instance.NewLinkDragged();
            }

            if(CanvasManager.Instance._redoButton.interactable == true)
            {
                CanvasManager.Instance._redoButton.interactable = false;
            }

            if (CanvasManager.Instance._undoButton.interactable == false)
            {
                CanvasManager.Instance._undoButton.interactable = true;
            }

            CreateDummy();
        }
        

    }
    public void OnDrag(PointerEventData eventdata)
    {
        if (IsDragable)
        {
            transform.SetParent(_content.transform);
            transform.position = eventdata.position;
            PlaceDummyInContent(dummy);
        }
            
    }
    public void OnEndDrag(PointerEventData eventdata)
    {
        // Debug.Log("OnEndDrag");
        if(IsDragable)
        {
            transform.SetSiblingIndex(dummy.transform.GetSiblingIndex());
            siblingIndex = transform.GetSiblingIndex();
            DestroyDummy();
            RecreateLink();
            if(transform.name== "LinkThink"|| transform.name == "LinkWatch")
            {
                transform.Find("GoToDisplay").gameObject.SetActive(true);
            }
            
            CanvasManager.Action action_type = transform.GetComponent<ActionInfo>().actionType;
            CanvasManager.Instance.PushIntoUndoStack((int)action_type);
        }
      
        IsDragable = false;
    }

 

    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.parent == _content.transform && !selected)
        {
            selected = true;
            if (CanvasManager.Instance._drag2PreviousScript != null)
            {
                CanvasManager.Instance._drag2PreviousScript.selected = false;
                CanvasManager.Instance._drag2PreviousScript.thisImage.color = Color.white;
            }
            CanvasManager.Instance._drag2PreviousScript = this;
            thisImage.color = Color.green;
            
        }
    }

  


    /// <summary>
    //Recreate the original link
    /// </summary>
    void RecreateLink()
    {
        
        CanvasManager.Action actionType = transform.GetComponent<ActionInfo>().actionType;
        GameObject newLink = Instantiate(gameObject, originalParent);
        newLink.name = _name;
        LayoutElement newLinkLayout = newLink.GetComponent<LayoutElement>();
        RectTransform rect = newLink.GetComponent<RectTransform>();
        newLinkLayout.enabled = false;
        rect.sizeDelta = new Vector2(PREFERRED_WIDTH, PREFERRED_HEIGHT);
        newLink.transform.Find("GoToDisplay").gameObject.SetActive(false);
        newLink.transform.GetComponent<Image>().color = Color.white;
        newLink.transform.SetSiblingIndex((int)actionType);
        

    }

    /// <summary>
    //Create a dummy gameobject to be placed in ChainInspector Window
    /// </summary>
    void CreateDummy()
    {
        dummy = new GameObject();

        dummy.transform.SetParent(_content.transform);
        LayoutElement _layElement = dummy.AddComponent<LayoutElement>();
        _layElement.preferredHeight = PREFERRED_HEIGHT;
        _layElement.preferredWidth = PREFERRED_WIDTH;
        _layElement.flexibleHeight = 0;
        _layElement.flexibleWidth = 0;

        dummy.transform.SetSiblingIndex(transform.GetSiblingIndex());
    }

    /// <summary>
    //Destroy the dummy gameobject
    /// </summary>
    void DestroyDummy()
    {
        Destroy(dummy);
        //Destroy(linksLibrary_Dummy);
    }

    /// <summary>
    // move dummy w.r.t dragged object
    /// </summary>

    void PlaceDummyInContent(GameObject dummy)
    {
        int newIndex = _content.transform.childCount;
        //if (_content.transform.childCount > 0)
        {
            for (int i = 0; i < _content.transform.childCount; i++)
            {
                if (transform.position.x < _content.transform.GetChild(i).position.x)
                {
                    newIndex = i;
                    if (dummy.transform.GetSiblingIndex() < newIndex)
                    {
                        newIndex--;

                    }
                    break;
                }
            }
        }

        //Debug.Log(_content.transform.childCount);
        dummy.transform.SetSiblingIndex(newIndex);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
