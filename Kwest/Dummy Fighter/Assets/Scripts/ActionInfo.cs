using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInfo : MonoBehaviour {

    // Use this for initialization
    public CanvasManager.Action actionType;
	void Start ()
    {
        CheckActionType();

    }

    void CheckActionType()
    {
        if(transform.name == "LinkWatch")
        {
            actionType = CanvasManager.Action.WATCH;
        }
        if (transform.name == "LinkAttack")
        {
            actionType = CanvasManager.Action.ATTACK;
        }
        if (transform.name == "LinkDodge")
        {
            actionType = CanvasManager.Action.DODGE;
        }
        if (transform.name == "LinkThink")
        {
            actionType = CanvasManager.Action.THINK;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
