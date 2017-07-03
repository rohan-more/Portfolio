namespace AISandbox
{
    class CollectTreasure : Leaf
    {
        public override BehaviorTree.Result Process()
        {
            if (!GameManager.gameManager.simpleActorScript.isPathCalculated)
            {
                if (BitManipulator.IsBitClear(GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber))
                {
                    Pathfollowing.FindUnblockedNodes();
                    Pathfollowing.AssignStartAndEndNodes();
                    Pathfollowing.SetActorPositions();
                    GameManager.gameManager.pathfindingScript.pathNotFound = true;
                    if (Pathfollowing.StartPathFinding())
                    {
                        GameManager.gameManager.simpleActorScript.currentPoint = 1;
                        GameManager.gameManager.simpleActorScript.isApproachingTarget = true;
                        GameManager.gameManager.simpleActorScript.isPathCalculated = true;
                        GameManager.gameManager.isANewkeyCollected = true;
                    }
                    else
                    {
              
                        EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Collect Treasure Leaf failed as " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber + " is not accessible");
                        GameManager.gameManager.specialNodeNumber++;
                        return BehaviorTree.Result.FAILURE;
                    }
                    EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Collect Treasure Leaf is running for " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber);
                    return BehaviorTree.Result.RUNNING;
                }
                else
                {                   
                    EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Collect Treasure Leaf failed as " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber + " is already collected");
                    GameManager.gameManager.specialNodeNumber++;
                    return BehaviorTree.Result.FAILURE;
                }
            }

            if (GameManager.gameManager.simpleActorScript.isPathCalculated && GameManager.gameManager.simpleActorScript.isApproachingTarget)
            {
                return BehaviorTree.Result.RUNNING;
            }
            else
            {   
                EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Collect Treasure Leaf succeeded in collecting " + (GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber);
                GameManager.gameManager.specialNodeNumber++;
                GameManager.gameManager.simpleActorScript.isPathCalculated = false;
                return BehaviorTree.Result.SUCCESS;
            }
        }
    }
}
