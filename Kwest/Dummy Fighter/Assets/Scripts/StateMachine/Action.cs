using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace State_Machine
{
    public class Action : State
    {
        private const string _name = "ActionState";
        public override string Name
        {
            get
            {
                return _name;
            }
        }
       
        public override void Execute()
        {


        }

    }
}