using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using State_Machine;
public class FightManager : MonoBehaviour {

    // Use this for initialization
    Animator _fightAnim;
    Animator _dummyAnim;
    GameObject _fightImage;
    Button _exitButton;
    Button _replayButton;



    void Start ()
    {
        _fightImage  = GameObject.Find("Canvas").transform.Find("FightImage").gameObject;
        _fightAnim = _fightImage.GetComponent<Animator>();
        _exitButton = GameObject.Find("Canvas").transform.Find("ButtonExit").GetComponent<Button>();
        _replayButton = GameObject.Find("Canvas").transform.Find("ButtonReplay").GetComponent<Button>();
        _dummyAnim = GameObject.Find("DummyPlayer").transform.Find("Dummy").GetComponent<Animator>();
        _dummyAnim.SetBool("hasEncounterEnded", true);


        _exitButton.onClick.AddListener(isExitButtonClicked);
        _replayButton.onClick.AddListener(isReplayButtonClicked);


    }
	
    void PlayFightAnimation()
    {
        if(_dummyAnim.GetBool("hasEncounterEnded"))
        {
            _fightAnim.Play("FightAnimation");
        }

    }

    void isExitButtonClicked()
    {
        Application.Quit();
    }

    void isReplayButtonClicked()
    {

    }


    // Update is called once per frame
    void Update ()
    {

    }
}
