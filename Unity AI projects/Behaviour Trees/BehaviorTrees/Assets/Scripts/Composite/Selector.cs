using System;

namespace AISandbox
{
    public class Selector : Composite
    {
        private int i = 0;
        private int totalKeys = 0;
        private BehaviorTree.Result result;
        private BehaviorTree.Result previousResult = BehaviorTree.Result.FAILURE;
        public override void Init()
        {
            totalKeys = keys.Length - 1;
        }
        public override BehaviorTree.Result Process()
        {
            object myObject = EntryPoint.behaviorTree.dataContext[keys[i]];
            Type type = myObject.GetType();
            if (type.Equals(typeof(CollectKey)))
            {
                result = ((CollectKey)myObject).Process();
            }
            else if (type.Equals(typeof(OpenDoor)))
            {
                result = ((OpenDoor)myObject).Process();
            }

            if (result == BehaviorTree.Result.RUNNING)
            {
                if (previousResult != BehaviorTree.Result.RUNNING)
                {
                    EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Selector running for " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber);
                }
                previousResult = result;
            }
            else if (result == BehaviorTree.Result.FAILURE)
            {
                if (i < totalKeys)
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Selector failed for " + (GridNode.GridNodeType)(GameManager.gameManager.specialNodeNumber - 1));
                previousResult = result;
            }
            else
            {
                if (i < totalKeys)
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Selector succeeded for " + (GridNode.GridNodeType)(GameManager.gameManager.specialNodeNumber - 1));
                previousResult = result;
            }
            return result;
        }
    }
}
