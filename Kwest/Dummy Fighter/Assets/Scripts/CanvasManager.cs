using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasManager : MonoBehaviour {

    // Use this for initialization

    public enum Action { THINK = 1, WATCH, ATTACK, DODGE };
    public Draggable2 _drag2PreviousScript;
 
    public Stack<int> Redo_List;
    public Stack<int> Undo_List;
    public List<Action> Action_List;
    GameObject _content;
    Button _fightButton;
    public Button _undoButton;
    public Button _redoButton;
    public Button _plusButton;
    public Button _minusButton;

    Button _exitButton;
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
        _fightButton = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("ButtonFight").GetComponent<Button>();
        _undoButton = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("ButtonUndo").GetComponent<Button>();
        _redoButton = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("ButtonRedo").GetComponent<Button>();
        _plusButton = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("LinksLibrary").transform.Find("IconPlus").transform.Find("Button").GetComponent<Button>();
        _minusButton = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("LinksLibrary").transform.Find("IconMinus").transform.Find("Button").GetComponent<Button>();
        _exitButton = GameObject.Find("Canvas").transform.Find("MainPanel").transform.Find("ButtonClose").GetComponent<Button>();

        _drag2PreviousScript = null;
        Redo_List = new Stack<int>();
        Undo_List = new Stack<int>();
        Action_List = new List<Action>();
        _redoButton.interactable = false;
        _undoButton.interactable = false;


        _fightButton.onClick.AddListener(FightButtonClicked);
        _undoButton.onClick.AddListener(UndoButtonClicked);
        _redoButton.onClick.AddListener(RedoButtonClicked);
        _plusButton.onClick.AddListener(PlusButtonClicked);
        _minusButton.onClick.AddListener(MinusButtonClicked);
        _exitButton.onClick.AddListener(ExitButtonClicked);


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

    public void EmptyUndoStack()
    {
        Undo_List.Clear();
    }

   void FightButtonClicked()
    {
        ListOfActions();
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.LoadScene("Fight");
    }

    void PlusButtonClicked()
    {
        _drag2PreviousScript.IdleValue++;
        _drag2PreviousScript.AttackValue++;
        _drag2PreviousScript.DodgeValue++;
        if (_drag2PreviousScript.selected && _drag2PreviousScript.transform.Find("GoToDisplay").gameObject.activeSelf)
        {
            _drag2PreviousScript.transform.Find("GoToDisplay").transform.Find("InCaseIdle").transform.Find("TextValue").GetComponent<Text>().text = _drag2PreviousScript.IdleValue.ToString();
            _drag2PreviousScript.transform.Find("GoToDisplay").transform.Find("InCaseAttack").transform.Find("TextValue").GetComponent<Text>().text = _drag2PreviousScript.AttackValue.ToString();
            _drag2PreviousScript.transform.Find("GoToDisplay").transform.Find("InCaseDodge").transform.Find("TextValue").GetComponent<Text>().text = _drag2PreviousScript.DodgeValue.ToString();
        }
       
    }

    void MinusButtonClicked()
    {
        _drag2PreviousScript.IdleValue--;
        _drag2PreviousScript.AttackValue--;
        _drag2PreviousScript.DodgeValue--;

        if (_drag2PreviousScript.selected && _drag2PreviousScript.transform.Find("GoToDisplay").gameObject.activeSelf)
        {
            _drag2PreviousScript.transform.Find("GoToDisplay").transform.Find("InCaseIdle").transform.Find("TextValue").GetComponent<Text>().text = _drag2PreviousScript.IdleValue.ToString();
            _drag2PreviousScript.transform.Find("GoToDisplay").transform.Find("InCaseAttack").transform.Find("TextValue").GetComponent<Text>().text = _drag2PreviousScript.AttackValue.ToString();
            _drag2PreviousScript.transform.Find("GoToDisplay").transform.Find("InCaseDodge").transform.Find("TextValue").GetComponent<Text>().text = _drag2PreviousScript.DodgeValue.ToString();
        }

    }





    void UndoButtonClicked()
    {
        int num = Undo_List.Pop();
        _redoButton.interactable = true;
        PushIntoRedoStack(num);
        MoveDeletedLink(num);

    }

    void RedoButtonClicked()
    {
        if(_redoButton.interactable == true)
        {
           Redo_List.Pop();
            RevertDeletedLink();
        }
        
    }

    void ExitButtonClicked()
    {
        Application.Quit();
    }

    void MoveDeletedLink(int num)
    {
        num--;
        _content.transform.GetChild(num).SetParent (transform);
    }

    void RevertDeletedLink()
    {
        GameObject temp = transform.GetChild(transform.childCount-1).gameObject;
        temp.transform.SetParent(_content.transform);
        temp.transform.SetSiblingIndex(temp.transform.GetComponent<Draggable2>().siblingIndex);
        

    }

    public void NewLinkDragged()
    {
        //EmptyRedoStack();
        //EmptyUndoStack();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void ListOfActions()
    {
        foreach (Transform child in _content.transform)
        {
            Action_List.Add(child.transform.GetComponent<ActionInfo>().actionType);
        }
    }


 void CollectActionData()
    {
         for(int i=0;i< _content.transform.childCount;i++)
        {
            //if (_content.transform.childCount > 0)
            {
                CanvasManager.Action action_type = _content.transform.GetChild(i).GetComponent<ActionInfo>().actionType;
                PushIntoUndoStack((int)action_type);
            }
        }
      
    }
 






    // Update is called once per frame
    void Update ()
    {
		
	}
}
