using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    public class Watch : State
    {

        private const string _name = "WatchState";
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

