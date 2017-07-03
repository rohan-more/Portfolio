namespace AISandbox
{
    public class CollectKeys : State
    {
        private const string _name = "CollectKeys";
        public override string Name
        {
            get
            {
                return _name;
            }
        }
        public override void Enter()
        {
            base.Enter();
            GameManager.gameManager.specialNodeNumber = 0;
        }
        public override void Execute()
        {
            if (!CreatingGridAndActor.simpleActorScript.isPathCalculated && GameManager.gameManager.specialNodeNumber < 3)
            {
                if (BitManipulator.IsBitClear(GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber))
                {
                    Pathfollowing.FindUnblockedNodes();
                    Pathfollowing.AssignStartAndEndNodes();
                    Pathfollowing.SetActorPositions();
                    CreatingGridAndActor.pathfindingScript.pathNotFound = true;
                    if (Pathfollowing.StartPathFinding())
                    {
                        CreatingGridAndActor.simpleActorScript.currentPoint = 1;
                        CreatingGridAndActor.simpleActorScript.isPathCalculated = true;
                        GameManager.gameManager.isANewkeyCollected = true;
                        BitManipulator.SetBit(ref GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber);
                    }
                    GameManager.gameManager.specialNodeNumber++;
                }
                else
                {
                    GameManager.gameManager.specialNodeNumber++;
                }
            }
            else if (!CreatingGridAndActor.simpleActorScript.isPathCalculated && GameManager.gameManager.specialNodeNumber == 3)
            {
                EntryPoint.stateMachine.SetActiveState("ChooseNextState");
            }
        }
    }
}
