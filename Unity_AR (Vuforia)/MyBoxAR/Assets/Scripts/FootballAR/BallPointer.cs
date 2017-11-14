using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPointer : MonoBehaviour
{

    // Use this for initialization

    Vector3 leftPost, rightPost;
    public bool changeDirection;
    GameObject arrowPivot;
    CanvasManager2 canvasManager2Script;


    Quaternion fromRotation, toRotation;
    void Start()
    {
        leftPost = GameObject.Find("FootballGoal").transform.Find("Left").gameObject.transform.position;
        rightPost = GameObject.Find("FootballGoal").transform.Find("Right").gameObject.transform.position;
        //arrowPivot = GameObject.Find("Pivot")..gameObject;

        fromRotation = Quaternion.AngleAxis(-25, transform.up);
        toRotation = Quaternion.AngleAxis(25, transform.up);
        canvasManager2Script = GameObject.Find("Canvas").GetComponent<CanvasManager2>();

    }


    void CalculateBallDirection()
    {

       if(!canvasManager2Script.isShootPressed)
        {
            float t = Mathf.PingPong(Time.time, 1.0f);
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, t);
        }
        else if(canvasManager2Script.isShootPressed)
        {
            canvasManager2Script.BallDirection();
        }
        

    }


   

    // Update is called once per frame
    void Update()
    {
       
        CalculateBallDirection();

            //transform.rotation = arrowPivot.transform.rotation;

    }
}
