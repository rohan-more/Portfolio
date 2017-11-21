//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using State_Machine;
//public class EntryPoint : MonoBehaviour
//{

//    // Use this for initialization
//    public static StateMachine playerStateMachine;
//    public static StateMachine enemyStateMachine;
//   // public int playerCurrentStateNumber;
//  //  public int enemyCurrentStateNumber;

//    private void Start()
//    {
//        playerStateMachine = new StateMachine(GameObject.Find("DummyPlayer").transform.Find("Dummy").GetComponent<Animator>());
//        enemyStateMachine = new StateMachine(GameObject.Find("Enemy").transform.Find("Dummy").GetComponent<Animator>());
//       // playerCurrentStateNumber = 0;
//       // enemyCurrentStateNumber = 0;

//        Attack attackScript = new Attack();
//        Idle idleScript = new Idle();
//        Dodge dodgeScript = new Dodge();
//        Watch watchScript = new Watch();
//        NextState nextStateScript = new NextState();
//        Think thinkScript = new Think();

//        playerStateMachine.AddState(idleScript);
//        playerStateMachine.AddState(attackScript);
//        playerStateMachine.AddState(dodgeScript);
//        playerStateMachine.AddState(watchScript);
//        playerStateMachine.AddState(nextStateScript);
//        playerStateMachine.AddState(thinkScript);

//        enemyStateMachine.AddState(idleScript);
//        enemyStateMachine.AddState(attackScript);
//        enemyStateMachine.AddState(dodgeScript);
//        enemyStateMachine.AddState(watchScript);
//        enemyStateMachine.AddState(nextStateScript);
//        enemyStateMachine.AddState(thinkScript);




//        playerStateMachine.SetActiveState("IdleState");
//        enemyStateMachine.SetActiveState("IdleState");


//    }
	
//	// Update is called once per frame
//	void Update ()
//    {
//        playerStateMachine.Execute();
//        enemyStateMachine.Execute();
//    }
//}
