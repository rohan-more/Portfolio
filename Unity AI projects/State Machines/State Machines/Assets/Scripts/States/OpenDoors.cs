namespace AISandbox
{
    public class OpenDoors : State
    {
        private const string _name = "OpenDoors";
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
            GameManager.gameManager.specialNodeNumber = 3;
        }
        public override void Execute()
        {
            if (!CreatingGridAndActor.simpleActorScript.isPathCalculated && GameManager.gameManager.specialNodeNumber < 6)
            {
                if (BitManipulator.IsBitSet(GameManager.gameManager.controlbits, (byte)(GameManager.gameManager.specialNodeNumber - 3)))
                {
                    if (BitManipulator.IsBitClear(GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber))
                    {
                        Pathfollowing.FindUnblockedNodes();
                        Pathfollowing.AddTheDoorNodeToTheUnblockedList();
                        Pathfollowing.AssignStartAndEndNodes();
                        Pathfollowing.SetActorPositions();
                        CreatingGridAndActor.pathfindingScript.pathNotFound = true;
                        if (Pathfollowing.StartPathFinding())
                        {
                            CreatingGridAndActor.simpleActorScript.currentPoint = 1;
                            CreatingGridAndActor.simpleActorScript.isPathCalculated = true;
                            GameManager.gameManager.isANewDoorOpened = true;
                            BitManipulator.SetBit(ref GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber);
                        }
                        GameManager.gameManager.specialNodeNumber++;
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
            else if (!CreatingGridAndActor.simpleActorScript.isPathCalculated && GameManager.gameManager.specialNodeNumber == 6)
            {
                EntryPoint.stateMachine.SetActiveState("ChooseNextState");
            }
        }
    }
}
