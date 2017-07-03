namespace AISandbox
{
    public class ChooseNextState : State
    {
        private const string _name = "ChooseNextState";
        public override string Name
        {
            get
            {
                return _name;
            }
        }
        public override void Execute()
        {
            if (GameManager.gameManager.isANewDoorOpened)
            {
                GameManager.gameManager.isANewDoorOpened = false;
                GameManager.gameManager.areAllKeysCollected =
                    BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 0) &&
                    BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 1) &&
                    BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 2);
                if (GameManager.gameManager.areAllKeysCollected)
                {
                    GameManager.gameManager.areAllDoorsOpened =
                        BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 3) &&
                        BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 4) &&
                        BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 5);
                    if (GameManager.gameManager.areAllDoorsOpened)
                    {
                        EntryPoint.stateMachine.SetActiveState("CollectTreasure");
                    }
                    else
                    {
                        EntryPoint.stateMachine.SetActiveState("OpenDoors");
                    }
                }
                else
                {
                    EntryPoint.stateMachine.SetActiveState("CollectKeys");
                }
            }
            else if (GameManager.gameManager.isANewkeyCollected)
            {
                GameManager.gameManager.isANewkeyCollected = false;
                GameManager.gameManager.areAllDoorsOpened =
                BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 3) &&
                BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 4) &&
                BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 5);
                if (GameManager.gameManager.areAllDoorsOpened)
                {
                    EntryPoint.stateMachine.SetActiveState("CollectTreasure");
                }
                else
                {
                    EntryPoint.stateMachine.SetActiveState("OpenDoors");
                }
            }
            else
            {
                EntryPoint.stateMachine.SetActiveState("CollectTreasure");
            }
        }
    }
}
