using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    public class Think : State
    {

        private const string _name = "ThinkState";
        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override void Execute(Animator anim)
        {


        }
    }
}

