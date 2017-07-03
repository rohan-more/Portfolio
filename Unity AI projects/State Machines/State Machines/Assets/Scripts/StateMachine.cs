using UnityEngine;
using System.Collections.Generic;

namespace AISandbox
{
    public class StateMachine
    {
        private Dictionary<string, State> states;
        private State _currentState = null;
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
        public StateMachine()
        {
            states = new Dictionary<string, State>();
        }
        public void AddState(State state)
        {
            states.Add(state.Name, state);
        }

        public bool RemoveState(State state)
        {
            return states.Remove(state.Name);
        }

        public bool  RemoveState(string name)
        {
            return states.Remove(name);
        }

        public void SetActiveState(State state)
        {
            try
            {
                currentState = states[state.Name];
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
                currentState.Execute();
            }
            else
            {
                Debug.Log("Current State is null");
            }          
        }
    }
}

