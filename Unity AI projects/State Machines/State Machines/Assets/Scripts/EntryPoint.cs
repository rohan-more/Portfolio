using UnityEngine;

namespace AISandbox
{
    public class EntryPoint : MonoBehaviour
    {
        public static StateMachine stateMachine;

        private void Start()
        {
            stateMachine = new StateMachine();

            CreatingGridAndActor creatingGrid = new CreatingGridAndActor();
            DrawingWalls drawingWalls = new DrawingWalls();
            PlacingGameObjects placingGameObjects = new PlacingGameObjects();
            ChooseNextState chooseNextState = new ChooseNextState();
            CollectKeys collectKeys = new CollectKeys();
            OpenDoors openDoors = new OpenDoors();
            CollectTreasure collectTreasure = new CollectTreasure();
            DoNothing doNothing = new DoNothing();
            

            stateMachine.AddState(creatingGrid);
            stateMachine.AddState(drawingWalls);
            stateMachine.AddState(placingGameObjects);
            stateMachine.AddState(chooseNextState);
            stateMachine.AddState(collectKeys);
            stateMachine.AddState(openDoors);
            stateMachine.AddState(collectTreasure);
            stateMachine.AddState(doNothing);
           

            stateMachine.SetActiveState("CreatingGridAndActor");
        }

        private void Update()
        {
            stateMachine.Execute();
        }
    }
}

