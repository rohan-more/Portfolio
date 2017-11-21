using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State_Machine;
public class DummyController : MonoBehaviour {

    // Use this for initialization

   // Animator anim;
    public  StateMachine _thisStateMachine;
    private void Start()
    {

        _thisStateMachine = new StateMachine(GetComponent<Animator>());

     
        Watch watchScript = new Watch();
        Think thinkScript = new Think();

        _thisStateMachine.AddState(watchScript);
        _thisStateMachine.AddState(thinkScript);


        _thisStateMachine.SetActiveState("IdleState");


    }

    // Update is called once per frame
    void Update()
    {
        _thisStateMachine.Execute();

    }

    public void AttackRtnEnd()
    {
        Debug.Log("AttackFwdEnd");
        _thisStateMachine._anim.SetBool("attackStart", false);

        //_thisStateMachine.SetActiveState("IdleState");

        _thisStateMachine.currentStateNumber++;
    }

    public void DodgeRtnEnd()
    {
        Debug.Log("DOdgeEnd");
        _thisStateMachine._anim.SetBool("dodgeStart", false);

       // _thisStateMachine.SetActiveState("IdleState");

        _thisStateMachine.currentStateNumber++;
    }

    public void IdleEnd()
    {
        Debug.Log("IdleEnd");
       

        switch (CanvasManager.Instance.Action_List[_thisStateMachine.currentStateNumber])
        {
            case CanvasManager.Action.ATTACK:
                Debug.Log("p Attack");
                //_thisStateMachine.SetActiveState("AttackState");
                _thisStateMachine._anim.SetBool("attackStart", true);
                //EntryPoint.playerStateMachine.GetAnimator().SetBool("attackStart", false);
                //EntryPoint.playerStateMachine.SetActiveState("AttackState");
                break;
            case CanvasManager.Action.DODGE:
                Debug.Log("p Dodge");
                _thisStateMachine._anim.SetBool("dodgeStart", true);
                // EntryPoint.playerStateMachine.SetActiveState("DodgeState");
                break;
            case CanvasManager.Action.THINK:
                Debug.Log("p Think");
                //  EntryPoint.playerStateMachine.SetActiveState("ThinkState");
                
                break;
            case CanvasManager.Action.WATCH:
                Debug.Log("p Watch");
                _thisStateMachine.SetActiveState("WatchState");
                //  EntryPoint.playerStateMachine.SetActiveState("WatchState");
                break;

        }


    }

 
}
