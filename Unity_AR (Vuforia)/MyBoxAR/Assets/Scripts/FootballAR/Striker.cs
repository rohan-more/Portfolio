using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : MonoBehaviour {

    // Use this for initialization

    Animator anim;
    Ball ballScript;
    CanvasManager2 canvasManager2Script;
    void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
        canvasManager2Script = GameObject.Find("Canvas").GetComponent<CanvasManager2>();
        ballScript = GameObject.Find("Football").GetComponent<Ball>();


    }

    public void Move()
    {
        
        if (canvasManager2Script.isShootPressed)
        {
            
            ballScript.isDirectionSet = true;
        }
       
    }


    public void ResetValues()
    {
        anim.SetBool("isShootPressed", false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
