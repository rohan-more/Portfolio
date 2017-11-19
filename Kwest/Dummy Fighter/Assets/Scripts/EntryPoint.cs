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

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
