namespace AISandbox
{
    public class CollectTreasure : State
    {
        private const string _name = "CollectTreasure";
        public override string Name
        {
            get
            {
                return _name;
            }
        }
        private const byte SUCCESS_CODE = 0xFC;
        public override void Enter()
        {
            base.Enter();
            GameManager.gameManager.specialNodeNumber = 6;
        }
        public override void Execute()
        {
            if (!CreatingGridAndActor.simpleActorScript.isPathCalculated && GameManager.gameManager.specialNodeNumber == 6)
            {
                if (GameManager.gameManager.controlbits == SUCCESS_CODE)
                {
                    Pathfollowing.FindUnblockedNodes();
                    Pathfollowing.AssignStartAndEndNodes();
                    Pathfollowing.SetActorPositions();
                    CreatingGridAndActor.pathfindingScript.pathNotFound = true;
                    if (Pathfollowing.StartPathFinding())
                    {
                        CreatingGridAndActor.simpleActorScript.currentPoint = 1;
                        CreatingGridAndActor.simpleActorScript.isPathCalculated = true;
                        BitManipulator.SetBit(ref GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber);
                        
                        GameManager.gameManager.specialNodeNumber++;
                       
                    }
                    else
                    {
                        CanvasManager.message.text = "Mission Failed";
                        EntryPoint.stateMachine.SetActiveState("NoProgressiveState");
                    }
                }
                else
                {
                    CanvasManager.message.text = "Mission Failed";
                    EntryPoint.stateMachine.SetActiveState("NoProgressiveState");
                }
            }
            else
            {               
                EntryPoint.stateMachine.SetActiveState("NoProgressiveState");
            }
        }
    }
}