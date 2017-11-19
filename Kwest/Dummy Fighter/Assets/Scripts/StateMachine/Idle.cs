using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    public class Idle : State
    {

        private const string _name = "IdleState";
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
