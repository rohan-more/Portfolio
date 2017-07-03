using UnityEngine;
using UnityEngine.UI;

namespace AISandbox
{
    public class CanvasManager : MonoBehaviour
    {
        private static Dropdown _menuDropdown;
        private static Toggle _debugToggle;

        public static Text panelName;
        public static Text currentFSMState;
        public static Text message;
        public static Text notification;
        public static DragHandler[] dragHandlerScripts;

        private void Awake()
        {
            GameObject canvas = GameObject.Find("Canvas");
            _menuDropdown = canvas.transform.FindChild("MenuDropdown").GetComponent<Dropdown>();
            _debugToggle = canvas.transform.FindChild("DebugToggle").GetComponent<Toggle>();
            panelName = canvas.transform.FindChild("Panel Name").GetComponent<Text>();
            currentFSMState = canvas.transform.FindChild("Current FSM State").GetComponent<Text>();
            message = canvas.transform.FindChild("Message").GetComponent<Text>();
            notification = canvas.transform.FindChild("Notification").GetComponent<Text>();
            _menuDropdown.onValueChanged.AddListener(OnDropdownMenuValueChanged);
            _debugToggle.onValueChanged.AddListener(OnDebugToggleValueChanged);

            GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
            dragHandlerScripts = new DragHandler[slots.Length];
            for (int i = 0; i < slots.Length; i++)
            {
                dragHandlerScripts[i] = slots[i].transform.GetChild(0).GetComponent<DragHandler>();
            }
        }
        private void OnDebugToggleValueChanged(bool isDebugMode)
        {
            CreatingGridAndActor.simpleActorScript.DrawDebugData = isDebugMode;
        }

        private void OnDropdownMenuValueChanged(int option)
        {
            switch (option)
            {
                case 1:
                    GameManager.gameManager.Reset();
                    GameManager.gameManager.isResetNeeded = false;
                    EntryPoint.stateMachine.SetActiveState("DrawingWalls");
                    break;
                case 0:
                    GameManager.gameManager.Reset();
                    GameManager.gameManager.isResetNeeded = false;
                    EntryPoint.stateMachine.SetActiveState("PlacingGameObjects");
                    break;
                case 2:
                    if (GameManager.gameManager.AreAllObjectsInScene())
                    {
                        GameManager.gameManager.isResetNeeded = true;
                        panelName.text = "Player Inventory";
                        EntryPoint.stateMachine.SetActiveState("ChooseNextState");
                    }
                    else
                    {
                        currentFSMState.text = "";
                        message.text = "Place all GameObjects in the grid before finding path";
                        EntryPoint.stateMachine.SetActiveState("NoProgressiveState");
                    }
                    break;
            }
        }
    }
}