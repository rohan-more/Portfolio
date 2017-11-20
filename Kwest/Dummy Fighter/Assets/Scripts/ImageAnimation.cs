using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAnimation : MonoBehaviour {

    // Use this for initialization
    Animator _fightAnim;
    Animator _dummyAnim;
    GameObject _fightImage;
    void Start ()
    {
        _fightImage = GameObject.Find("Canvas").transform.Find("FightImage").gameObject;
        _fightAnim = _fightImage.GetComponent<Animator>();
        _dummyAnim = GameObject.Find("DummyPlayer").transform.Find("Dummy").GetComponent<Animator>();
        _dummyAnim.SetBool("hasEncounterEnded", true);
       // _fightAnim.enabled = false;


    }
	
    public void AnimationEnded()
    {
        //_fightAnim.enabled = true;
        if (_dummyAnim.GetBool("hasEncounterEnded"))
            {
                _fightAnim.Play("FightAnimation");
            }
             _dummyAnim.SetBool("hasEncounterEnded", false);
            _fightImage.SetActive(false);
        
    }

	// Update is called once per frame
	void Update () {
       

    }
}
