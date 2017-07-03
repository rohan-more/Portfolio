using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace AISandbox
{
    public class EntryPoint : MonoBehaviour
    {     
        public static MyList<string> messages = new MyList<string>();
        public static BehaviorTree behaviorTree;

        private CollectTreasure collectTreasure;

        private CollectKey collectRedKey;
        private CollectKey collectGreenKey;
        private CollectKey collectBlueKey;

        private OpenDoor openRedDoor;
        private OpenDoor openGreenDoor;
        private OpenDoor openBlueDoor;

        private Selector collectKeySelector;
        private Selector openDoorSelector;

        private Sequence rootSequence;

        BehaviorTree.Result result;
        BehaviorTree.Result previousResult = BehaviorTree.Result.UNKNOWN;
        public static bool doNotRecordAnymoreMessages = false;
        private void Awake()
        {
            GameManager.ResetGameManager();

            collectTreasure = new CollectTreasure();

            collectRedKey = new CollectKey();
            collectGreenKey = new CollectKey();
            collectBlueKey = new CollectKey();

            openRedDoor = new OpenDoor();
            openGreenDoor = new OpenDoor();
            openBlueDoor = new OpenDoor();

            collectKeySelector = new Selector();
            collectKeySelector.keys = new string[] { "CollectRedKey", "CollectGreenKey", "CollectBlueKey" };
            collectKeySelector.Init();

            openDoorSelector = new Selector();
            openDoorSelector.keys = new string[] { "OpenRedDoor", "OpenGreenDoor", "OpenBlueDoor" };
            openDoorSelector.Init();

            rootSequence = new Sequence();
            rootSequence.keys = new string[] { "CollectKeySelector", "OpenDoorSelector", "CollectTreasure" };

            behaviorTree = new BehaviorTree(rootSequence);

            behaviorTree.AddToDictionary("RootSequence", rootSequence);

            behaviorTree.AddToDictionary("CollectKeySelector", collectKeySelector);
            behaviorTree.AddToDictionary("OpenDoorSelector", openDoorSelector);
            behaviorTree.AddToDictionary("CollectTreasure", collectTreasure);

            behaviorTree.AddToDictionary("OpenRedDoor", openRedDoor);
            behaviorTree.AddToDictionary("OpenGreenDoor", openGreenDoor);
            behaviorTree.AddToDictionary("OpenBlueDoor", openBlueDoor);

            behaviorTree.AddToDictionary("CollectRedKey", collectRedKey);
            behaviorTree.AddToDictionary("CollectGreenKey", collectGreenKey);
            behaviorTree.AddToDictionary("CollectBlueKey", collectBlueKey);
        }
        private void Update()
        {
            if (GameManager.gameManager.gamestate == GameManager.GameStates.PathFinding)
            {
                result = behaviorTree.Process();

                if (result == BehaviorTree.Result.SUCCESS && !doNotRecordAnymoreMessages)
                {
                   
                }

                if (previousResult != BehaviorTree.Result.RUNNING && result == BehaviorTree.Result.RUNNING && !doNotRecordAnymoreMessages)
                {
                    messages.Add((messages.Count).ToString() + ": Doing preparations in order to collect treasure");
                }
                else if (previousResult != BehaviorTree.Result.FAILURE && result == BehaviorTree.Result.FAILURE && !doNotRecordAnymoreMessages)
                {
                    doNotRecordAnymoreMessages = true;
                    CanvasManager.message.text = "Mission failed";
                    messages.Add((messages.Count).ToString() + ": Treasure cannot be collected");
                }
              
                previousResult = result;
            }
        }
    }
}
