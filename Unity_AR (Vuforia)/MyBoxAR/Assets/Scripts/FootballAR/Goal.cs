using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

    // Use this for initialization
    public Text _scoreText;
    public int scoreCounter;
    CanvasManager2 canvasManager2Script;
    Ball ballScript;
	void Start ()
    {
        canvasManager2Script = GameObject.Find("Canvas").GetComponent<CanvasManager2>();
        ballScript = GameObject.Find("Football").GetComponent<Ball>();

    }

    private void OnTriggerEnter(Collider other)
    {
        scoreCounter++;
        _scoreText.text = scoreCounter.ToString();
        canvasManager2Script.goal.text = "Goal!";
        canvasManager2Script.ResetEverything();
        


    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
