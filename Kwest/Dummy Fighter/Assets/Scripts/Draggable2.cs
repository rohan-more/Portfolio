using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    // Use this for initialization
    GameObject _content;
    const float PREFERRED_HEIGHT = 111.93f;
    const float PREFERRED_WIDTH = 214.87f;
    //GameObject linksLibrary_Dummy;
    GameObject dummy;

    GameObject canvas;
     Transform originalParent;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        _content = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("ChainInspector").transform.Find("Viewport").transform.Find("Content").gameObject;
        originalParent = transform.parent;
       // linksLibrary_Dummy = new GameObject();
       //linksLibrary_Dummy.name = "LinkLibrary";


    }


    public void OnBeginDrag(PointerEventData eventdata)
    {
        transform.SetParent(canvas.transform);
        LayoutElement newLinkLayout = GetComponent<LayoutElement>();
        newLinkLayout.enabled = true;
        newLinkLayout.preferredHeight = PREFERRED_HEIGHT;
        newLinkLayout.preferredWidth = PREFERRED_WIDTH;
        newLinkLayout.flexibleHeight = 0;
        newLinkLayout.flexibleWidth = 0;

       // CreateDummy(linksLibrary_Dummy);
        CreateDummy();

    }
    public void OnDrag(PointerEventData eventdata)
    {
       transform.SetParent(_content.transform);
        transform.position = eventdata.position;

        Debug.Log(transform.name);

     
        PlaceDummyInContent(dummy);
    }
    public void OnEndDrag(PointerEventData eventdata)
    {
        // Debug.Log("OnEndDrag");
        transform.SetSiblingIndex(dummy.transform.GetSiblingIndex());
        DestroyDummy();
        CanvasManager.Action actionType = transform.GetComponent<ActionInfo>().actionType;
       GameObject newLink = Instantiate(gameObject, originalParent);
        LayoutElement newLinkLayout = newLink.GetComponent<LayoutElement>();
        RectTransform rect = newLink.GetComponent<RectTransform>();
        newLinkLayout.enabled = false;
        rect.sizeDelta = new Vector2(PREFERRED_WIDTH, PREFERRED_HEIGHT); 
        //newLinkLayout.minWidth = 0;
        //newLinkLayout.minHeight = 0;
        //newLinkLayout.flexibleHeight = 0;
        //newLinkLayout.preferredWidth = 0;
        //newLinkLayout.flexibleHeight = 0;
        //newLinkLayout.flexibleWidth = 0;
        newLink.transform.SetSiblingIndex((int)actionType);

       


    }

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

    void DestroyDummy()
    {
        Destroy(dummy);
        //Destroy(linksLibrary_Dummy);
    }


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

        Debug.Log(_content.transform.childCount);
        dummy.transform.SetSiblingIndex(newIndex);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
