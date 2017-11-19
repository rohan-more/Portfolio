using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    // Use this for initialization

    public enum Action { THINK = 1, WATCH, ATTACK, DODGE };
    public Draggable2 _drag2PreviousScript;
    Stack<int> Redo_List;
    Stack<int> Undo_List;
    GameObject _content;
    private static CanvasManager _instance;
    public static CanvasManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<CanvasManager>(); ;
            }
            return _instance;
        }

    }
    void Start ()
    {
        _content = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("ChainInspector").transform.Find("Viewport").transform.Find("Content").gameObject;
        _drag2PreviousScript = null;
    }
    /// <summary>
    //Push into Undo Stack
    /// </summary>
    public void PushIntoUndoStack(int number)
    {
        Undo_List.Push(number);
    }
    /// <summary>
    //Push into Redo Stack
    /// </summary>
    public void PushIntoRedoStack(int number)
    {
        Redo_List.Push(number);
    }
    /// <summary>
    //Empty Redo Stack whenever new link dragged
    /// </summary>
    public void EmptyRedoStack() 
    {
        Redo_List.Clear();
    }

 
 






    // Update is called once per frame
    void Update ()
    {
		
	}
}
