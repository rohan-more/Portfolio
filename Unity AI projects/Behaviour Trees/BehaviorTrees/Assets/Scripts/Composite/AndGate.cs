using System;
using System.Collections.Generic;

namespace AISandbox
{
    class AndGate : Composite
    {
        public override BehaviorTree.Result Process()
        {
            foreach (string key in keys)
            {
                object myObject = EntryPoint.behaviorTree.dataContext[key];
                Type type = myObject.GetType();
                BehaviorTree.Result result;
                if (type.Equals(typeof(Inverter)))
                {
                    result = ((Inverter)myObject).Process();
                    if (result != BehaviorTree.Result.SUCCESS)
                    {
                        EntryPoint.messages.Add(key + result);
                        return result;
                    }
                }
                else if (type.Equals(typeof(IsKeyAccesibleThenCollect)))
                {
                    result = ((IsKeyAccesibleThenCollect)myObject).Process();
                    if (result != BehaviorTree.Result.SUCCESS)
                    {
                        EntryPoint.messages.Add(key + result);
                        return result;
                    }
                }
                else if (type.Equals(typeof(IsDoorAccessibleThenOpen)))
                {
                    result = ((IsDoorAccessibleThenOpen)myObject).Process();
                    if (result != BehaviorTree.Result.SUCCESS)
                    {
                        EntryPoint.messages.Add(key + result);
                        return result;
                    }
                }
                else if (type.Equals(typeof(OrGate)))
                {
                    result = ((OrGate)myObject).Process();
                    if (result != BehaviorTree.Result.SUCCESS)
                    {
                        EntryPoint.messages.Add(key + result);
                        return result;
                    }
                }
            }
            return BehaviorTree.Result.SUCCESS;
        }
    }
}
