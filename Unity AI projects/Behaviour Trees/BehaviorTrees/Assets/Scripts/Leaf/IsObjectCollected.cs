using System;
using System.Collections.Generic;

namespace AISandbox
{
    class IsObjectCollected : Leaf
    {
        public override BehaviorTree.Result Process()
        {
            if (BitManipulator.IsBitSet(GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber))
            {
                return BehaviorTree.Result.SUCCESS;
            }
            else
            {
                return BehaviorTree.Result.FAILURE;
            }
        }
    }
}
