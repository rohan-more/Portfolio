﻿namespace State_Machine
{
    public abstract class State
    {
       
        public abstract string Name { get; }
        public virtual void Enter()
        {
            //CanvasManager.currentFSMState.text = "FSM State: " + EntryPoint.stateMachine.GetActiveStateName();
        }
        public virtual void Execute(UnityEngine.Animator ANIM) { }
    }
}