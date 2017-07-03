using System;
using UnityEngine;

namespace AISandbox
{
    class RepeatUntilFail : Decorator
    {
        private BehaviorTree.Result result;

        public override BehaviorTree.Result Process()
        {
            object myObject = EntryPoint.behaviorTree.dataContext[keys[0]];
            Type type = myObject.GetType();
            if (type.Equals(typeof(AndGate)))
            {
                result = ((AndGate)myObject).Process();
            }
            if(result == BehaviorTree.Result.FAILURE)
            {
                return BehaviorTree.Result.SUCCESS;
            }
            return BehaviorTree.Result.RUNNING;
        }
    }
}
