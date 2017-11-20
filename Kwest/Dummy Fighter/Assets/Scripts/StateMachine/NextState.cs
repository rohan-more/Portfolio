using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    public class NextState : State
    {

        private const string _name = "NextState";
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

