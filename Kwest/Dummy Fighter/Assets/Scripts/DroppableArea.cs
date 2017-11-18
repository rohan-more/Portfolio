using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	// Use this for initialization
	void Start () {
		
	}
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem dragScript = eventData.pointerDrag.GetComponent<DraggableItem>();
        if(dragScript != null)
        {
            dragScript.originalParent = this.transform;
        }


    }

	// Update is called once per frame
	void Update () {
		
	}
}
