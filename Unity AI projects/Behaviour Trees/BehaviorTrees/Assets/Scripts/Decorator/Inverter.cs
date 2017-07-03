using System;
using System.Collections.Generic;

namespace AISandbox
{
    public class Inverter : Decorator
    {
        private BehaviorTree.Result result;
        public override BehaviorTree.Result Process()
        {
            object myObject = EntryPoint.behaviorTree.dataContext[keys[0]];
            Type type = myObject.GetType();
            if (type.Equals(typeof(IsObjectCollected)))
            {
               result  = ((IsObjectCollected)myObject).Process();
            }    
            else if (type.Equals(typeof(RepeatUntilFail)))
            {
                result = ((RepeatUntilFail)myObject).Process();
            }  
            if(result == BehaviorTree.Result.SUCCESS)
            {
                return BehaviorTree.Result.FAILURE;
            }
            else if (result == BehaviorTree.Result.FAILURE)
            {
                return BehaviorTree.Result.SUCCESS;
            }
            else
            {
                return BehaviorTree.Result.RUNNING;
            }
        }
    }
}
