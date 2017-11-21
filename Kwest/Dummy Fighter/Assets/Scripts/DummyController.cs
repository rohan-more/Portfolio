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

    }

    // Update is called once per frame
    void Update()
    {
        _thisStateMachine.Execute();

    }

    public void AttackRtnEnd()
    {
        _thisStateMachine._anim.SetBool("attackStart", false);
    }

    public void DodgeRtnEnd()
    {
        _thisStateMachine._anim.SetBool("dodgeStart", false);
    }

    public void IdleEnd()
    {
     
       
        if(gameObject.transform.parent.name == "DummyPlayer" && _thisStateMachine.currentStateNumber<CanvasManager.Instance.Action_List.Count )
        {
            switch (CanvasManager.Instance.Action_List[_thisStateMachine.currentStateNumber])
            {
                case CanvasManager.Action.ATTACK:
                    _thisStateMachine.currentStateNumber++;
                    _thisStateMachine._anim.SetBool("attackStart", true);
                    Debug.Log(gameObject.transform.parent.name + " " + _thisStateMachine.currentStateNumber);
                    break;
                case CanvasManager.Action.DODGE:
                    _thisStateMachine.currentStateNumber++;
                    _thisStateMachine._anim.SetBool("dodgeStart", true);
                    Debug.Log(gameObject.transform.parent.name + " " + _thisStateMachine.currentStateNumber);
                    break;
                case CanvasManager.Action.THINK:
                    _thisStateMachine.currentStateNumber++;
                    Debug.Log(gameObject.transform.parent.name + " " + _thisStateMachine.currentStateNumber);
                    break;
                case CanvasManager.Action.WATCH:
                    _thisStateMachine.currentStateNumber++;
                    Debug.Log(gameObject.transform.parent.name + " " + _thisStateMachine.currentStateNumber);
                    Watch();
                    break;

            }
        }

        else if(_thisStateMachine.currentStateNumber < CanvasManager.Instance.Test_Enemy.Count && (gameObject.transform.parent.name == "Enemy"))
        {
            switch (CanvasManager.Instance.Test_Enemy[_thisStateMachine.currentStateNumber])
            {
                case CanvasManager.Action.ATTACK:
                    _thisStateMachine._anim.SetBool("attackStart", true);
                    _thisStateMachine.currentStateNumber++;
                    break;
                case CanvasManager.Action.DODGE:
                    _thisStateMachine._anim.SetBool("dodgeStart", true);
                    _thisStateMachine.currentStateNumber++;
                    break;
                case CanvasManager.Action.THINK:
                    _thisStateMachine.currentStateNumber++;
                    break;
                case CanvasManager.Action.WATCH:
                    _thisStateMachine.currentStateNumber++;
                    Watch();
                    break;

            }
        }
        


    }

    int Chance()
    {
        int chance = Random.Range(1, 100);
        return chance;
    }


    void Watch()
    {
        
        int watchChance = Chance();

        if(watchChance <50)
        {
            CanvasManager.Action enemyAction = CanvasManager.Instance.Test_Enemy[_thisStateMachine.currentStateNumber + 1];
            switch (enemyAction)
            {
                case CanvasManager.Action.ATTACK:
                    if (CanvasManager.Instance.Action_List[_thisStateMachine.currentStateNumber + 1] != CanvasManager.Action.DODGE)
                    {
                        CanvasManager.Instance.Action_List[_thisStateMachine.currentStateNumber + 1] = CanvasManager.Action.DODGE;
                    
                    }
                    break;

            }
        }
       

    }



 
}
