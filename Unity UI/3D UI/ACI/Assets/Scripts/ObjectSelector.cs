using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour {

    public GameObject shape;
    private Button thisButton;
    public bool isActive;
    private CanvasManager canvas;
    private LoginScreen _loginSw;
    // Use this for initialization
    void Start ()
    {
        canvas = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        thisButton = GetComponentInParent<Button>();
        thisButton.onClick.AddListener(OnClick);
        isActive = true;
        _loginSw = GameObject.Find("LoginControl").GetComponent<LoginScreen>();
    }
    void OnClick()
    {
        for (var i = 0; i < canvas.objectList.Count; i++)
        {
            if (canvas.objectList[i] != canvas.currentShape)
            {
                canvas.objectList[i].SetActive(false);
            }
        }
        if (this.isActive ==true)
        {
            shape.SetActive(false);
            isActive = false;
        }
        else if (this.isActive == false)
        {
            shape.SetActive(true);
            isActive = true;
            canvas.currentShape = shape;
         
            _loginSw.sw.WriteLine(canvas.currentShape+"selected");
        }
        

    }
        // Update is called once per frame
	void Update ()
    {
		
	}
}
