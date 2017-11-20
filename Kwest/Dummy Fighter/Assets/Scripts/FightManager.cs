using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State_Machine;
public class FightManager : MonoBehaviour {

    // Use this for initialization
    Animator _fightAnim;
    Animator _dummyAnim;
    GameObject _fightImage;


    void Start ()
    {
        _fightImage  = GameObject.Find("Canvas").transform.Find("FightImage").gameObject;
        _fightAnim = _fightImage.GetComponent<Animator>();
        _dummyAnim = GameObject.Find("DummyPlayer").transform.Find("Dummy").GetComponent<Animator>();
        _dummyAnim.SetBool("hasEncounterEnded", true);
    }
	
    void PlayFightAnimation()
    {
        if(_dummyAnim.GetBool("hasEncounterEnded"))
        {
            _fightAnim.Play("FightAnimation");
            Debug.Log("xvcvxcv");

        }
        Debug.Log("hiiii");
       // _dummyAnim.SetBool("hasEncounterEnded", false);
        //_fightImage.SetActive(false);
    }


	// Update is called once per frame
	void Update ()
    {
        //if(_fightImage.activeSelf)
        //{
        //    PlayFightAnimation();
        //}
        

    }
}
