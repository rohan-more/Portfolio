using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class SpawnController : MonoBehaviour {

    // Use this for initialization
    private bool mPositionSet;
    GameObject ball;
    GameObject projectile;
    GameObject goal;
    GameObject goalPost;
    public bool isBallDestroyed = false;
    public bool isGoalDestroyed = false;
    public bool isBallReleased = false;
    public bool isPickedUp = false;
    public bool isRayHit = false;
    public bool hasBallReached = false;
    private bool hasBallLaunched = false;
    Transform newCam;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    Transform spawnPos;
    CanvasManager _canvasManagerScript;
 
    void Start ()
    {
        
        ball = Resources.Load("Sphere") as GameObject;
        goal = Resources.Load("Goal") as GameObject;
        newCam = GameObject.Find("ARCamera").transform;
        _canvasManagerScript = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        CreateBall();
        CreateGoal();



    }



 private void CreateBall()
    {
         projectile = Instantiate(ball) as GameObject;
  
        projectile.transform.position = Camera.main.transform.forward * Random.Range(20, 30);
        hasBallLaunched = false;
        isBallDestroyed = false;
    }

    private void CreateGoal()
    {
        Vector3 goalPosition = (Camera.main.transform.forward + new Vector3(0, 0, 50));
        Quaternion goalRotation = Quaternion.identity;
        goalRotation.eulerAngles = new Vector3(0, 90, 0);
        goalPost = Instantiate(goal, (Random.insideUnitSphere * 10) + goalPosition, goalRotation) as GameObject;
        //goalPost.transform.LookAt(Camera.main.transform);
        //Camera.main.transform.LookAt(goalPost.transform);

        //goalPost.transform.position = Camera.main.transform.up * Random.Range(30, 70);

        isGoalDestroyed = false;
    }



    private void PickupBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (projectile.GetComponent<Collider>().Raycast(ray, out hit, 100.0F))
        {
            journeyLength = Vector3.Distance(projectile.transform.position, -(Camera.main.transform.up) * 3 + Camera.main.transform.forward * 10);
            isRayHit = true;
            
        }
        isPickedUp = true;
    }

    private void PlaceBall()
    {
        if (isRayHit)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fracJourney = distCovered / journeyLength;
            projectile.transform.position = Vector3.Lerp(projectile.transform.position, -(Camera.main.transform.up) * 3 + Camera.main.transform.forward * 10, fracJourney);
        }
        
        if (!isBallDestroyed)
        {
            if (projectile.transform.position == -(Camera.main.transform.up) * 3 + Camera.main.transform.forward * 10 )
            {
                hasBallReached = true;
            }
        }
       
    }

    private void MoveBallWithCamera()
    {
        //projectile.transform.position = -(Camera.main.transform.up) * 3 + Camera.main.transform.forward * 10;
        projectile.transform.position = Vector3.Lerp(projectile.transform.position, -(newCam.up) * 3 + newCam.forward * 10, Time.deltaTime* 5);
    }


    private void ThrowBall()
    {
        hasBallLaunched = true;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        
        rb.velocity = Camera.main.transform.forward * 30;
        isBallReleased = true;
       
    }

    private void MoveGoalSideways()
    {
        if (!isGoalDestroyed)
        {
            goalPost.transform.RotateAround(Vector3.zero, Vector3.up, 5 * Time.deltaTime);
        }

    }
   

    
    // Update is called once per frame
    void Update ()
    {
       
       
        if (isBallDestroyed)
        {
            CreateBall();
        }
        if (isGoalDestroyed)
        {
            CreateGoal();
        }

        if (!hasBallReached)
        {
            PlaceBall();
        }

        if (Input.GetMouseButtonDown(0) && hasBallReached)
        {
            
            ThrowBall();
        }

        if (Input.GetMouseButtonDown(0) &&  _canvasManagerScript.isPanelDeactivated)
        {
            PickupBall();
            
        }

        if(isPickedUp && !hasBallLaunched)
        {
            MoveBallWithCamera();
        }
       // if(_canvasManagerScript.isPanelDeactivated)
        {
            MoveGoalSideways();
        }
        

    }
}
