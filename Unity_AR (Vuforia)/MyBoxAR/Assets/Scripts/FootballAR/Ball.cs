using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    BallPointer ballPointerScript;
    Rigidbody rbBall;
    TrailRenderer trail;
    Animator anim;
    public bool hasSpawned = false;
   public bool isDirectionSet = false;
    public bool hasBallHit = false;
    public bool hasBallReset;
    Vector3 position;
    GoalKeeper goalKeeperScript;
    CanvasManager2 canvasManager2Script;
    GameObject goal;

    // Use this for initialization
    void Start () {
        ballPointerScript = GameObject.Find("Pivot").GetComponent<BallPointer>();
        rbBall = GameObject.Find("Football").GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        trail = gameObject.GetComponent<TrailRenderer>();
        goal = GameObject.Find("FootballGoal").gameObject;
        goalKeeperScript = GameObject.Find("xbot@Goalkeeper Idle").GetComponent<GoalKeeper>();
        canvasManager2Script = GameObject.Find("Canvas").GetComponent<CanvasManager2>();
        position = transform.position;
        //ResetBall();
    }

    public void KickBall()
    {
        if (isDirectionSet)
        {
            anim.enabled = true;
            trail.enabled = true;
            rbBall.drag = 0;
            rbBall.angularDrag = 0;
            rbBall.AddForce(-transform.right * 2, ForceMode.Impulse);
            
            goalKeeperScript.Decide();

        }
        //if(hasBallHit)
        //{
        //    anim.enabled = false;
        //    trail.enabled = false;
        //}
    }

    //IEnumerator ResetBall()
    //{
    //    if(hasBallHit)
    //    {
    //        yield return new WaitForSeconds(3);

    //        {
    //            ResetValues();
    //        }
            
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "FootballGoal")
        {
            rbBall.drag = 6;
            rbBall.angularDrag =6;
            canvasManager2Script.goal.text = "Miss!";


        }

        if (collision.gameObject.name == "mixamorig:Spine")
        {
            rbBall.drag = 6;
            rbBall.angularDrag = 6;
            canvasManager2Script.goal.text = "Save!";
        }
        

        hasBallHit = true;
        
        canvasManager2Script.ResetEverything();
    }

   
    public void ResetValues()
    {
        
        isDirectionSet = false;
        anim.enabled = false;
        trail.enabled = false;
        transform.position = position;
       // transform.rotation = Quaternion.identity;
        rbBall.drag = 100;
        rbBall.angularDrag = 100;
       
        //rbBall.velocity = Vector3.zero;
        hasBallHit = false;

        if (transform.position == position)
        {
            hasBallReset = true;
        }
    }
 

    // Update is called once per frame
    void Update ()
    {
		if(Vector3.Distance(transform.position, goal.transform.position) > 55.0f)
        {
            rbBall.drag = 6;
            rbBall.angularDrag = 6;
            canvasManager2Script.goal.text = "Miss!";
            hasBallHit = true;
        }

        if(hasBallHit)
        {

            canvasManager2Script.ResetEverything();
            ResetValues();
        }
	}
}
