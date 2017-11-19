using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    public class Dodge : State
    {

        private const string _name = "DodgeState";
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

