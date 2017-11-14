using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager2 : MonoBehaviour {

    //GameObject footBall;
    //GameObject projectile;

    Ball ballScript;
    Transform pivot;
    Transform ball;
    Striker strikerScript;
    GoalKeeper goalKeeperScript;
    Animator anim;
    public bool isShootPressed = false;
    public bool isBallSpawned= true;
    public bool resetAll;
    public Text goal;
    int timer = 0;
    // Use this for initialization
    void Start ()
    {

        anim = GameObject.Find("ybot@Soccer Penalty Kick").GetComponent<Animator>();
        pivot = GameObject.Find("Pivot").GetComponent<Transform>();
        ball = GameObject.Find("Football").GetComponent<Transform>();
        ballScript = GameObject.Find("Football").GetComponent<Ball>();
        strikerScript = GameObject.Find("ybot@Soccer Penalty Kick").GetComponent<Striker>();
        goalKeeperScript = GameObject.Find("xbot@Goalkeeper Idle").GetComponent<GoalKeeper>();

    }
	


    public void ShootPressed()
    {
        isShootPressed = true;

        anim.SetBool("isShootPressed", true);

        goal.text = "";
    }


    public void BallDirection()
    {
       
        ball.rotation = pivot.rotation;

        ballScript.KickBall();

    }

    public void ResetEverything()
    {
        {
            goalKeeperScript.ResetValues();
            strikerScript.ResetValues();
            ballScript.ResetValues();
            if (isShootPressed == true)
            {
                isShootPressed = false;
            }
     
        }
 
    }

	// Update is called once per frame
	void Update ()
    {

    }
}
