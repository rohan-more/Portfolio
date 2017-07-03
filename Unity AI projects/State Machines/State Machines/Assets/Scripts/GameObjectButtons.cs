using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace AISandbox
{
    public class GameObjectButtons : MonoBehaviour
    {

        // Use this for initialization

        private RectTransform MyPanelRect;
        private GameObject MyPanel;
        public Button GreenKey;
        public Button RedKey;
        public Button BlueKey;
        public Button GreenDoor;
        public Button BlueDoor;
        public Button RedDoor;
        public Button Treasure;
        private Button Reset;
        private GridNode[,] _nodes;
        public bool placeGreenKey = false;
        public bool placeRedKey = false;
        public bool placeBlueKey = false;
        public bool placeGreenDoor = false;
        public bool placeBlueDoor = false;
        public bool placeRedDoor = false;
        public bool placeTreasure = false;

        private GridNode gridNodeObject;


        void Start()
        {
            MyPanel = GameObject.Find("My Panel");
            MyPanelRect = MyPanel.transform.GetComponent<RectTransform>();
            GreenKey = MyPanelRect.GetChild(1).GetComponent<Button>();
            RedKey = MyPanelRect.GetChild(0).GetComponent<Button>();
            BlueKey = MyPanelRect.GetChild(2).GetComponent<Button>();
            GreenDoor = MyPanelRect.GetChild(5).GetComponent<Button>();
            BlueDoor = MyPanelRect.GetChild(3).GetComponent<Button>();
            RedDoor = MyPanelRect.GetChild(4).GetComponent<Button>();
            Treasure = MyPanelRect.GetChild(6).GetComponent<Button>();
            Reset = MyPanelRect.GetChild(7).GetComponent<Button>();

            GreenKey.onClick.AddListener(OnCLickGreenKey);
            RedKey.onClick.AddListener(OnCLickRedKey);
            BlueKey.onClick.AddListener(OnCLickBlueKey);
            GreenDoor.onClick.AddListener(OnCLickGreenDoor);
            BlueDoor.onClick.AddListener(OnCLickBlueDoor);
            RedDoor.onClick.AddListener(OnCLickRedDoor);
            Treasure.onClick.AddListener(OnCLickTreasure);
            Reset.onClick.AddListener(OnCLickReset);

            gridNodeObject =  Resources.Load<GridNode>("Prefabs/GridNodePrefab");
        }

        // Update is called once per frame
        void Update()
        {
           

        }


        public void OnCLickGreenKey()
        {


            placeGreenKey = true;
            GreenKey.gameObject.SetActive(false);
        }
        public void OnCLickRedKey()
        {
            placeRedKey = true;
            RedKey.gameObject.SetActive(false);

        }
        public void OnCLickBlueKey()
        {
            placeBlueKey = true;
            BlueKey.gameObject.SetActive(false);

        }
        public void OnCLickGreenDoor()
        {

            placeGreenDoor = true;
            GreenDoor.gameObject.SetActive(false);

        }
        public void OnCLickRedDoor()
        {

            placeRedDoor = true;
            RedDoor.gameObject.SetActive(false);
        }
        public void OnCLickBlueDoor()
        {
            placeBlueDoor = true;
            BlueDoor.gameObject.SetActive(false);

        }
        public void OnCLickTreasure()
        {
            placeTreasure = true;
            Treasure.gameObject.SetActive(false);

        }
        public void OnCLickReset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.gameManager.ResetGameManager();

        }



    }
}
