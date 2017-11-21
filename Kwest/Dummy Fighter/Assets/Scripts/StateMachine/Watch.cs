using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State_Machine
{
    public class Watch : State
    {

        private const string _name = "WatchState";
        int choice;
        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override void Enter()
        {
            base.Enter();

            choice = Random.Range(1, 100);

            if(choice < 50)
            {

            }

            else
            {

            }


            Debug.Log("JIIII");

        }

        public override void Execute(Animator anim)
        {
          

        }

    }
}

