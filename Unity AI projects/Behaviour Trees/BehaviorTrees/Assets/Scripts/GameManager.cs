using UnityEngine;
using System.Collections.Generic;

namespace AISandbox
{
    public class GameManager
    {
        private readonly int NUMBER_OF_ROWS = 20;
        private readonly int NUMBER_OF_COLUMNS = 40;
        private GameObject actorPrefab;
        private static GameManager _gameManager;
        public static GameManager gameManager
        {
            get
            {
                if (_gameManager == null)
                {
                    _gameManager = new GameManager();
                }
                return _gameManager;
            }
        }
        public bool isResetNeeded = false;
        public bool isNodeFindingNeeded = true;
        public byte controlbits = 0;
        public bool isANewDoorOpened = true;
        public bool areAllKeysCollected = false;
        public bool isANewkeyCollected = false;
        public bool areAllDoorsOpened = false;
        public byte specialNodeNumber;
        public bool changeStartPoint;
      //  public Sprite road;
       // public Sprite forest;
       // public Sprite water;
       // public Sprite blockedSprite;
        public Sprite unlockedDoor;

        public static Color _waterColor;
        public static Color _forestColor;
        public static Color _roadColor;
        public static Color _blockedColor;

        public Grid grid;
        public GameObject actor;
        public Pathfinding pathfindingScript;
        public SimpleActor simpleActorScript;
        public GridNode startNode;
        public GridNode endNode;
        public enum GameStates { DrawingWalls, PlacingGameObjects, PathFinding, DoNothing }
        public GameStates gamestate = GameStates.PlacingGameObjects;
        private GameManager()
        {
            _waterColor = new Color(0.0f, 0.0f, 255.0f);
            _forestColor = new Color(0.0f, 128.0f, 0.0f);
            _roadColor = new Color(0.501f, 0.501f, 0.501f);
            _blockedColor = new Color(0.647f, 0.164f, 0.164f);
            unlockedDoor = Resources.Load<Sprite>("Sprites/door_open");

            grid = GameObject.Find("Grid").GetComponent<Grid>();
            CreateGrid(NUMBER_OF_ROWS, NUMBER_OF_COLUMNS);
            grid.transform.position = new Vector3(grid.size.x * -0.5f, grid.size.y * 0.5f);

            actorPrefab = Resources.Load<GameObject>("Prefabs/SimpleActor");
            CreateActor();
        }
        public bool AreAllObjectsInScene()
        {
            bool result = true;
            foreach (DragHandler dragHandlerScript in CanvasManager.dragHandlerScripts)
            {
                result = result && !dragHandlerScript.enabled;
                if (!result)
                {
                    return result;
                }
            }
            return result;
        }
        public static void ResetGameManager()
        {
            _gameManager = null;
        }

        public void Reset()
        {
            CanvasManager.message.text = "";
            if (isResetNeeded)
            {
                CanvasManager.panelName.text = "GameObjects";
                isNodeFindingNeeded = true;
                controlbits = 0;
                specialNodeNumber = 0;
                isANewDoorOpened = true;
                areAllKeysCollected = false;
                isANewkeyCollected = false;
                areAllDoorsOpened = false;
                changeStartPoint = false;
                simpleActorScript.isPathCalculated = false;
                startNode.isStartNode = false;
                startNode.isEndNode = false;
                endNode.isStartNode = false;
                endNode.isEndNode = false;
                startNode = endNode = null;
                if (pathfindingScript)
                {
                    pathfindingScript.pathNotFound = true;
                    pathfindingScript.openList.Clear();
                    pathfindingScript.closedList.Clear();
                }
                if (actor)
                {
                    actor.SetActive(false);
                }
                if (simpleActorScript)
                {
                    simpleActorScript.currentPoint = 1;
                }

                foreach (GridNode node in grid.nodes)
                {
                    node.f = node.g = node.h = 0;
                    node.parentNode = null;
                }

                foreach (DragHandler dragHandlerScript in CanvasManager.dragHandlerScripts)
                {
                    dragHandlerScript.gameObject.SetActive(true);
                    dragHandlerScript.enabled = true;
                    dragHandlerScript.gameObject.transform.position = dragHandlerScript.startPosition;
                    dragHandlerScript.canvasGroup.blocksRaycasts = true;
                }
                foreach (KeyValuePair<GridNode.GridNodeType, GridNode> node in grid.specialNodes)
                {
                    node.Value.gridNodeType = GridNode.GridNodeType.Normal;
                    node.Value.myRenderer.color = GridManipulation.originalColor;
                }
                grid.specialNodes.Clear();
            }
        }

        private void CreateGrid(int rows, int columns)
        {
            Vector2 node_position = new Vector2(grid.node_width * 0.5f, grid.node_height * -0.5f);
            grid.nodes = new GridNode[rows, columns];
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < columns; ++col)
                {
                    GridNode node = CreateNode(row, col);
                    node.transform.localPosition = node_position;
                    grid.nodes[row, col] = node;

                    node_position.x += grid.node_width;
                }
                node_position.x = grid.node_width * 0.5f;
                node_position.y -= grid.node_height;
            }
        }
        private GridNode CreateNode(int row, int col)
        {
            GridNode node = Object.Instantiate(grid.gridNodePrefab, grid.transform) as GridNode;
            node.name = string.Format("Node {0}{1}", (char)('A' + row), col);
            node.tag = "Node";
            node.row = row;
            node.column = col;
            node.terrainType = (GridNode.TerrainType)Random.Range(1, 4);
            if (node.terrainType == GridNode.TerrainType.Road)
            {
               // node.myRenderer.sprite = road;
                node.myRenderer.color = _roadColor;
            }
            else if (node.terrainType == GridNode.TerrainType.Forest)
            {
                //node.myRenderer.sprite = forest;
                node.myRenderer.color = _forestColor;
            }
            else
            {
                // node.myRenderer.sprite = water;
                node.myRenderer.color = _waterColor;
            }
            return node;
        }

        private void CreateActor()
        {
            actor = Object.Instantiate(actorPrefab) as GameObject;
            actor.name = "Actor";
            pathfindingScript = actor.GetComponent<Pathfinding>();
            simpleActorScript = actor.GetComponent<SimpleActor>();
            actor.SetActive(false);
        }
    }
}

