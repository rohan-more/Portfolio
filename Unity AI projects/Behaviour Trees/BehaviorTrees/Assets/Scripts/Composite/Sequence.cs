using System;
namespace AISandbox
{
    class Sequence : Composite
    {
        private BehaviorTree.Result result;
        public override BehaviorTree.Result Process()
        {
            if (!GameManager.gameManager.isANewDoorOpened && !GameManager.gameManager.isANewkeyCollected)
            {
                if (GameManager.gameManager.controlbits == 0xfc && GameManager.gameManager.specialNodeNumber == 6)
                {
                    object myObject = EntryPoint.behaviorTree.dataContext[keys[2]];
                    result = ((CollectTreasure)myObject).Process();
                    return result;
                }
                else
                {
                    return BehaviorTree.Result.FAILURE;
                }
            }
          
            if (GameManager.gameManager.specialNodeNumber >= 0 && GameManager.gameManager.specialNodeNumber <= 2)
            {
                if (GameManager.gameManager.isANewDoorOpened)
                {
                    object myObject = EntryPoint.behaviorTree.dataContext[keys[0]];
                    result = ((Selector)myObject).Process();
                }
            }
            else if (GameManager.gameManager.specialNodeNumber >= 3 && GameManager.gameManager.specialNodeNumber <= 5)
            {
                if (GameManager.gameManager.isANewkeyCollected)
                {
                    object myObject = EntryPoint.behaviorTree.dataContext[keys[1]];
                    result = ((Selector)myObject).Process();
                }
            }
            else
            {
                GameManager.gameManager.isANewkeyCollected = false;
                GameManager.gameManager.isANewDoorOpened = false;
            }
            return BehaviorTree.Result.RUNNING;
        }
    }
}
