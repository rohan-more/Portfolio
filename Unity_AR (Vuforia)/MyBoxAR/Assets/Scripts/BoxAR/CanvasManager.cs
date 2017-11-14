using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    // Use this for initialization

   
    private Text _scoreText;
    private GameObject _panel;
    private int scoreCounter = 0;
    public bool isPanelDeactivated = false;
 
	void Start ()
    {
        _panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
        _scoreText = GameObject.Find("Canvas").transform.Find("Text").transform.Find("Score").GetComponent<Text>();
        StartCoroutine(ActivatePanel());

    }


    IEnumerator ActivatePanel()
    {
        yield return new WaitForSeconds(15);

        _panel.SetActive(false);
        isPanelDeactivated = true;
    }

    public void PanelCloseButton()
    {
        if(_panel.activeSelf)
        {
            
            _panel.SetActive(false);
            isPanelDeactivated = true;
        }
    }

    public void ScoreUpdate()
    { 
            scoreCounter++;
            _scoreText.text = scoreCounter.ToString();
        
    }



	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            
           Application.Quit();

            return;
        }
    }
}
