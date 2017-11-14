using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    // Use this for initialization


    
    public bool isGoalHit = false;
    private bool isDestroyed = false;
    SpawnController spawnControllerScript;
    CanvasManager canvasManagerScript;
    
    void Start ()
    {
    

      
        spawnControllerScript = GameObject.Find("CubeSpawner").GetComponent<SpawnController>();
        canvasManagerScript = GameObject.Find("Canvas").GetComponent<CanvasManager>();
    }
    
    //void Explode()
    //{
        
    //    right.position += new Vector3(0.1f, 0,0 );
    //    left.position -= new Vector3(0.1f, 0, 0);
    //    down.position -= new Vector3(0, 0.1f, 0);
    //    up.position += new Vector3(0, 0.1f, 0);
       
    //}
    private void OnTriggerEnter(Collider other)
    {
        isGoalHit = true;
       
        canvasManagerScript.ScoreUpdate();
        
        spawnControllerScript.isGoalDestroyed = true;
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update ()
    {
        
    }
}
