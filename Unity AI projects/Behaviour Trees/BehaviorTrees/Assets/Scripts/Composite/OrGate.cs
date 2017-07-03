using System;
using System.Collections.Generic;
using UnityEngine;

namespace AISandbox
{
    class OrGate : Composite
    {
        private List<BehaviorTree.Result> results = new List<BehaviorTree.Result> ();

        public override BehaviorTree.Result Process()
        {
            foreach (string key in keys)
            {
                object myObject = EntryPoint.behaviorTree.dataContext[key];
                Type type = myObject.GetType();
                BehaviorTree.Result result;
                if (type.Equals(typeof(AndGate)))
                {
                    result = ((AndGate)myObject).Process();
                    if (result == BehaviorTree.Result.RUNNING)
                    {
                        return result;
                    }
                    results.Add(result);
                }
            }
            foreach (BehaviorTree.Result result in results)
            {
                if (result == BehaviorTree.Result.SUCCESS)
                {
                    return BehaviorTree.Result.SUCCESS;
                }      
            }
            return BehaviorTree.Result.FAILURE;
        }          
    }
}
