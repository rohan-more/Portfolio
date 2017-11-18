using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler  {

    // Use this for initialization
    GameObject _content;
    GameObject thinkItem;
    GameObject thinkPrefab;
    GameObject watchItem;
    GameObject watchPrefab;
    GameObject attackItem;
    GameObject attackPrefab;
    GameObject dodgeItem;
    GameObject dodgePrefab;
    public GameObject dummy;

    GameObject canvas;
    public Transform originalParent;
	void Start ()
    {
        canvas = GameObject.Find("Canvas");
        _content = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("ChainInspector").transform.Find("Viewport").transform.Find("Content").gameObject;
        thinkPrefab = Resources.Load("LinkThink") as GameObject;
        watchPrefab = Resources.Load("LinkWatch") as GameObject;
        attackPrefab = Resources.Load("LinkAttack") as GameObject;
        dodgePrefab = Resources.Load("LinkDodge") as GameObject;

        

    }


    public void OnBeginDrag(PointerEventData eventdata)
    {
        CreateDummy();

        if (transform.name == "LinkThink")
        {
            CreateThinkLink();   
        }
        if (transform.name == "LinkWatch")
        {
            CreateWatchLink();
        }
        if (transform.name == "LinkAttack")
        {
            CreateAttackLink();
        }

        if (transform.name == "LinkDodge")
        {
            CreateDodgeLink();
        }

    }
    public void OnDrag(PointerEventData eventdata)
    {
        //Debug.Log(transform.name);
        
        if (transform.name == "LinkWatch")
        {
            watchItem.transform.position = eventdata.position;
            watchItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        if (transform.name == "LinkThink")
        {
            thinkItem.transform.position = eventdata.position;
            thinkItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        if (transform.name == "LinkAttack")
        {
            attackItem.transform.position = eventdata.position;
            attackItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        if (transform.name == "LinkDodge")
        {
            dodgeItem.transform.position = eventdata.position;
            dodgeItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        CheckDummyPlacement();
        //PlaceDummyInContent();
    }
    public void OnEndDrag(PointerEventData eventdata)
    {
        // Debug.Log("OnEndDrag");
       
        if (transform.name == "LinkThink")
        {
            thinkItem.transform.SetParent(_content.transform);
            thinkItem.transform.SetSiblingIndex(dummy.transform.GetSiblingIndex());
        }
        if (transform.name == "LinkWatch")
        {
            watchItem.transform.SetParent(_content.transform);
            watchItem.transform.SetSiblingIndex(dummy.transform.GetSiblingIndex());
        }
        if (transform.name == "LinkAttack")
        {
            attackItem.transform.SetParent(_content.transform);
            attackItem.transform.SetSiblingIndex(dummy.transform.GetSiblingIndex());
        }
        if (transform.name == "LinkDodge")
        {
            dodgeItem.transform.SetParent(_content.transform);
            dodgeItem.transform.SetSiblingIndex(dummy.transform.GetSiblingIndex());
        }

        DestroDummy();
    }

    void CreateDummy()
    {
        dummy = new GameObject();
        dummy.transform.SetParent(_content.transform);
        LayoutElement _layElement = dummy.AddComponent<LayoutElement>();
        _layElement.preferredHeight = 111.93f;
        _layElement.preferredWidth =  214.87f;
        _layElement.flexibleHeight = 0;
        _layElement.flexibleWidth = 0;

        dummy.transform.SetSiblingIndex(transform.GetSiblingIndex());
    }

    void DestroDummy()
    {
        Destroy(dummy);
    }

    void CheckDummyPlacement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        //if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }



    void PlaceDummyInContent()
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


    private void CreateThinkLink()
    {
        thinkItem = Instantiate(thinkPrefab, canvas.transform) as GameObject;
    }
    private void CreateWatchLink()
    {
        watchItem = Instantiate(watchPrefab, canvas.transform) as GameObject;
    }
    private void CreateAttackLink()
    {
        attackItem = Instantiate(attackPrefab, canvas.transform) as GameObject;
    }
    private void CreateDodgeLink()
    {
        dodgeItem = Instantiate(dodgePrefab, canvas.transform) as GameObject;
    }


    //void OnMouseOver()


    // Update is called once per frame
    void Update ()
    {
        //CheckDummyPlacement();

    }
}
