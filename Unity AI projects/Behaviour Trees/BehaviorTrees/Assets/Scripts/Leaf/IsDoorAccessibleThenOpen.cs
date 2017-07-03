using System;
using System.Collections.Generic;

namespace AISandbox
{
    class IsDoorAccessibleThenOpen : Leaf
    {
        int currentSpecialNodeNumber = GameManager.gameManager.specialNodeNumber - 1;
        public override BehaviorTree.Result Process()
        {         
            if (!GameManager.gameManager.simpleActorScript.isPathCalculated && currentSpecialNodeNumber < GameManager.gameManager.specialNodeNumber)
            {
                if (BitManipulator.IsBitSet(GameManager.gameManager.controlbits, (byte)(GameManager.gameManager.specialNodeNumber - 3)))
                {
                    if (BitManipulator.IsBitClear(GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber))
                    {
                        Pathfollowing.FindUnblockedNodes();
                        Pathfollowing.AddTheDoorNodeToTheUnblockedList();
                        Pathfollowing.AssignStartAndEndNodes();
                        Pathfollowing.SetActorPositions();
                        GameManager.gameManager.pathfindingScript.pathNotFound = true;
                        if (Pathfollowing.StartPathFinding())
                        {
                            GameManager.gameManager.simpleActorScript.currentPoint = 1;
                            GameManager.gameManager.simpleActorScript.isPathCalculated = true;
                            GameManager.gameManager.isANewDoorOpened = true;
                            BitManipulator.SetBit(ref GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber);
                        }
                        else
                        {
                            return BehaviorTree.Result.FAILURE;
                        }
                        GameManager.gameManager.specialNodeNumber++;
                        return BehaviorTree.Result.RUNNING;
                    }
                    else
                    {
                        GameManager.gameManager.specialNodeNumber++;
                    }
                }
                else
                {
                    GameManager.gameManager.specialNodeNumber++;
                }
            }
            if (GameManager.gameManager.simpleActorScript.isPathCalculated && currentSpecialNodeNumber == (GameManager.gameManager.specialNodeNumber - 2))
            {
                return BehaviorTree.Result.RUNNING;
            }
            else
            {
                return BehaviorTree.Result.SUCCESS;
            }
        }
    }
}
