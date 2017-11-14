using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    // Use this for initialization
    SpawnController spawnControllerScript;
    TrailRenderer ballTrail;
	void Start () {
        spawnControllerScript = GameObject.Find("CubeSpawner").GetComponent<SpawnController>();
        ballTrail = this.GetComponent<TrailRenderer>();


    }
	
	// Update is called once per frame


	void Update () {
        if (spawnControllerScript.isBallReleased == true)
        {
            ballTrail.enabled = true;
            DestroyBall();
        }
        

    }

   
    private void DestroyBall()
    {
        if (Vector3.Distance(Camera.main.transform.position, gameObject.transform.position) > 85.0f)
        {
            spawnControllerScript.isRayHit = false;
            spawnControllerScript.hasBallReached = false;
            spawnControllerScript.isBallReleased = false;
            spawnControllerScript.isPickedUp = false;
            spawnControllerScript.isBallDestroyed = true;
            Destroy(gameObject);
        }
        
    }


}
