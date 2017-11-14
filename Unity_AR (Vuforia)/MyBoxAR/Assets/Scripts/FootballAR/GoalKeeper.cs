using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour {

    // Use this for initialization
    Animator anim;
    enum Decision
    {
          STAND, RIGHT, LEFT, _COUNT
    }
    void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame

    public void Decide()
    {
        Decision choice = (Decision)Random.Range(0, (int)Decision._COUNT);

        switch(choice)
        {
            case Decision.STAND:
                {
                    anim.SetBool("moveAhead", true);


                    break;
                }
            case Decision.LEFT:
                {
                    anim.SetBool("jumpLeft", true);


                    break;
                }
            case Decision.RIGHT:
                {
                    anim.SetBool("jumpRight", true);


                    break;
                }
        }
    }

    public void ResetValues()
    {
        anim.SetBool("moveAhead", false);
        anim.SetBool("jumpLeft", false);
        anim.SetBool("jumpRight", false);
    }

    void Update ()
    {
       

    }
}
