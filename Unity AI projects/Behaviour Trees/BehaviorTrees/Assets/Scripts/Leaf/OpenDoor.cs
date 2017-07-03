namespace AISandbox
{
    public class OpenDoor : Leaf
    {
        public override BehaviorTree.Result Process()
        {
            if (!GameManager.gameManager.simpleActorScript.isPathCalculated)
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
                            GameManager.gameManager.simpleActorScript.isApproachingTarget = true;
                            GameManager.gameManager.simpleActorScript.isPathCalculated = true;
                            GameManager.gameManager.isANewDoorOpened = true;
                        }
                        else
                        {
                            EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Open Door Leaf failed as " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber + " is not accessible");
                            GameManager.gameManager.specialNodeNumber++;                           
                            if (GameManager.gameManager.isANewDoorOpened && GameManager.gameManager.specialNodeNumber == 6)
                            {
                                GameManager.gameManager.specialNodeNumber = 0;
                            }
                            return BehaviorTree.Result.FAILURE;
                        }
                        EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Open Door Leaf is running for " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber);
                        return BehaviorTree.Result.RUNNING;
                    }
                    else
                    {
                        EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Open Door Leaf failed as " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber + " is already opened");
                        GameManager.gameManager.specialNodeNumber++;                     
                        if (GameManager.gameManager.isANewDoorOpened && GameManager.gameManager.specialNodeNumber == 6)
                        {
                            GameManager.gameManager.specialNodeNumber = 0;
                        }
                        return BehaviorTree.Result.FAILURE;
                    }
                }
                else
                {
                    EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Open Door Leaf failed as " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber + " cannot be opened because " + (GridNode.GridNodeType)(GameManager.gameManager.specialNodeNumber - 3) + " not collected");
                    GameManager.gameManager.specialNodeNumber++;                 
                    if (GameManager.gameManager.isANewDoorOpened && GameManager.gameManager.specialNodeNumber == 6)
                    {
                        GameManager.gameManager.specialNodeNumber = 0;
                    }
                    return BehaviorTree.Result.FAILURE;
                }
            }

            if (GameManager.gameManager.simpleActorScript.isPathCalculated && GameManager.gameManager.simpleActorScript.isApproachingTarget)
            {
                return BehaviorTree.Result.RUNNING;
            }
            else
            {
                EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Open Door Leaf succeeded in opening " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber);
                GameManager.gameManager.specialNodeNumber++;        
                GameManager.gameManager.simpleActorScript.isPathCalculated = false;
                if (GameManager.gameManager.isANewDoorOpened && GameManager.gameManager.specialNodeNumber == 6)
                {
                    GameManager.gameManager.specialNodeNumber = 0;
                }
                return BehaviorTree.Result.SUCCESS;
            }
        }
    }
}
