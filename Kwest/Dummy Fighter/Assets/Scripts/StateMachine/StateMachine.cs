using UnityEngine;
using System.Collections.Generic;


namespace State_Machine
{
    public class StateMachine
    {
        private Dictionary<string, State> states;
        private State _currentState = null;
        public Animator _anim;
        public int currentStateNumber;
        private State currentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
                _currentState.Enter();
            }
        }
        public StateMachine(Animator anim)
        {
            states = new Dictionary<string, State>();
            _anim = anim;
            currentStateNumber = 0;
        }
        public void AddState(State state)
        {
            states.Add(state.Name, state);
        }
        public Animator GetAnimator()
        {
            return _anim;
        }

        public bool RemoveState(State state)
        {
            return states.Remove(state.Name);
        }

        public bool RemoveState(string name)
        {
            return states.Remove(name);
        }

        public void SetActiveState(State state)
        {
            try
            {
                currentState = states[state.Name];
                currentState.Enter();
            }
            catch (KeyNotFoundException)
            {
                Debug.Log("Not a valid key");
            }
        }

        public void SetActiveState(string name)
        {
            try
            {
                currentState = states[name];
                currentState.Enter();
            }
            catch (KeyNotFoundException)
            {
                Debug.Log("Not a valid key");
            }
        }

        public string GetActiveStateName()
        {
            return currentState.Name;
        }

        public void Execute()
        {
            if (currentState != null)
            {
                currentState.Execute(_anim);
            }
            else
            {
                Debug.Log("Current State is null");
            }
        }
    }
}

