using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State_Machine;
public class EntryPoint : MonoBehaviour
{

    // Use this for initialization
    public static StateMachine stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine();

        Action actionScript = new Action();
        Idle idleScript = new Idle();
        Dodge dodgeScript = new Dodge();
        Watch watchScript = new Watch();
        NextState nextStateScript = new NextState();
        Think thinkScript = new Think();

        stateMachine.SetActiveState("Idle");


    }
	
	// Update is called once per frame
	void Update ()
    {
        stateMachine.Execute();
	}
}
