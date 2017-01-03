using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace AISandbox
{
    public class Pathfollowing : MonoBehaviour
    {
       

        private const int NUMBER_OF_ROWS = 30;
        private const int NUMBER_OF_COLUMNS = 40;

        private GridNode[] startNodes;
        private GridNode[] endNodes;
        private GridNode goalNode;
        
        //private GameObject[] actors;
        //private GameObject actorPrefab;
        //private Pathfinding[] PFScripts;
        //private SimpleActor[] SActor;


        private const float SPAWN_RANGE = 10.0f;

        //private FlockingController[] _flockingControllerScript;
        //private Flocking_SimpleActor[] _flockingSimpleActorScript;
        //private OrientedActor[] _orientedActorScript;
        //private SeekAndArriveController[] _seekArriveScript;
        private Dictionary<int, List<Vector3>> pathPoints;
        private Dictionary<int, List<GridNode.TerrainType>> pathPointsTerrainTypes;
        private List<GridNode> unblockedNodes;
        private Flocking flockingScript;
        private Grid grid;
    
        private Dropdown _menuDropdown;
        private Toggle _debugToggle;
        private Text _text;
        private bool isGoalNodeGiven;
        

        public enum Gamestates { Start, Flocking, DrawingTracks, DrawingPath, PathFinding }
        public Gamestates _gameState = Gamestates.Start;
        public Gamestates gamestate
        {
            get
            {
                return _gameState;
            }
            set
            {
                _gameState = value;
            }
        }


        private void Start()
        {
            grid = GameObject.Find("Grid").GetComponent<Grid>();
            // Create and center the grid
            grid.Create(NUMBER_OF_ROWS, NUMBER_OF_COLUMNS);
            flockingScript = GameObject.Find("Pathfollowing").GetComponent<Flocking>();
            Vector2 gridSize = grid.size;
            Vector2 gridPos = new Vector2(gridSize.x * -0.5f, gridSize.y * 0.5f);
            grid.transform.position = gridPos;

            startNodes = new GridNode[flockingScript.TOTALACTORS];
            endNodes = new GridNode[flockingScript.TOTALACTORS];
            goalNode = new GridNode();
            pathPoints = new Dictionary<int, List<Vector3>>(flockingScript.TOTALACTORS);
            pathPointsTerrainTypes = new Dictionary<int, List<GridNode.TerrainType>>(flockingScript.TOTALACTORS);
            for (int i = 0; i < flockingScript.TOTALACTORS; i++)
            {
                pathPoints[i] = new List<Vector3>();
                pathPointsTerrainTypes[i] = new List<GridNode.TerrainType>();
                
            }       
            unblockedNodes = new List<GridNode>();

           // actorPrefab = Resources.Load<GameObject>("Prefabs/SimpleActor");

           // CreateActors();
            GameObject canvas = GameObject.Find("Canvas");
            _menuDropdown = canvas.GetComponent<Transform>().FindChild("MenuDropdown").GetComponent<Dropdown>();
            _debugToggle = canvas.GetComponent<Transform>().FindChild("DebugToggle").GetComponent<Toggle>();
            _text = canvas.GetComponent<Transform>().FindChild("Text").GetComponent<Text>();
            _menuDropdown.onValueChanged.AddListener(OnDropdownMenuValueChanged);
            _debugToggle.onValueChanged.AddListener(OnDebugToggleValueChanged);
            _gameState = Gamestates.Flocking;
           
        }

      /*  private void CreateActors()
        {
            actors = new GameObject[TOTALACTORS];
            PFScripts = new Pathfinding[TOTALACTORS];
            SActor = new SimpleActor[TOTALACTORS];
            _flockingControllerScript = new FlockingController[TOTALACTORS];
            _flockingSimpleActorScript = new Flocking_SimpleActor[TOTALACTORS];
            _orientedActorScript = new OrientedActor[TOTALACTORS];
            _seekArriveScript = new SeekAndArriveController[TOTALACTORS];

           // _flockingActorPrefab = Instantiate<FlockingController>(_flockingActorPrefab);
            for (int i = 0; i < TOTALACTORS; i++)
            {
                actors[i] = Instantiate(actorPrefab, transform) as GameObject;
                actors[i].name = "Actor_" + (i + 1);
                PFScripts[i] = actors[i].GetComponent<Pathfinding>();
                SActor[i] = actors[i].GetComponent<SimpleActor>();
                _flockingControllerScript[i] = actors[i].GetComponent<FlockingController>();
                _flockingSimpleActorScript[i] = actors[i].GetComponent<Flocking_SimpleActor>();

                SActor[i].rollNumber = i;
                actors[i].SetActive(false);
            }
        }*/


        private void OnDebugToggleValueChanged(bool DebugModeOn)
        {
            int actor = 2;
            if (DebugModeOn)
            {
              flockingScript.SActor[(int)actor - 1].DrawDebugData = true;
            }
            else
            {
                
                for (int i = 0; i < flockingScript.TOTALACTORS; i++)
                {
                    flockingScript.SActor[i].DrawDebugData = false;
                }
            }
        }

        public void RecalculatePath(int i)
        {
            flockingScript.PFScripts[i].pathNotFound = true;
            AssignStartAndEndNodes(i);
            SetActorPositions(i);
            FindPath(i);
            flockingScript.SActor[i].currentPoint = 1;
            flockingScript.SActor[i].isPathCalculated = true;
        }



        private void OnDropdownMenuValueChanged(int option)
        {
            switch (option)
            {
                case 0:
                    _gameState = Gamestates.Start;




                    for (int i = 0; i < flockingScript.PLAYERACTORS; i++)
                    {
                            
                            flockingScript.player_actor[i].GetComponent<SimpleActor>().enabled = false;
                            flockingScript.player_actor[i].GetComponent<Flocking_SimpleActor>().enabled = false;
                            flockingScript.player_actor[i].GetComponent<FlockingController>().enabled = false;
                            flockingScript.player_actor[i].GetComponent<OrientedActor>().enabled = false;
                            flockingScript.player_actor[i].GetComponent<PlayerController>().enabled = false;
                        }
                    for (int i = 0; i < flockingScript.TOTALACTORS; i++)
                        {

                            flockingScript.actors[i].GetComponent<SimplePathFollowingActor>().enabled = false;
                            flockingScript.actors[i].GetComponent<Pathfinding>().enabled = false;
                            flockingScript.actors[i].GetComponent<SeekAndArriveController>().enabled = false;
                        }
                    

                    break;
                case 1:
                    _gameState = Gamestates.DrawingTracks;

                    for (int i = 0; i < flockingScript.TOTALACTORS; i++)
                    {

                        flockingScript.actors[i].GetComponent<SimplePathFollowingActor>().enabled = true;

                        flockingScript.actors[i].GetComponent<Pathfinding>().enabled = true;

                        flockingScript.actors[i].GetComponent<SeekAndArriveController>().enabled = true;
                    
                        if (flockingScript.PFScripts[i])
                        {
                            flockingScript.PFScripts[i].pathNotFound = true;
                            flockingScript.PFScripts[i].openList.Clear();
                            flockingScript.PFScripts[i].closedList.Clear();
                        }
                        if (flockingScript.actors[i])
                        {
                            //  flockingScript.actors[i].SetActive(false);
                        }
                        if (flockingScript.SActor[i])
                        {
                            flockingScript.SActor[i].currentPoint = 0;
                        }
                    }
                    foreach (GridNode node in grid.nodes)
                    {
                        node.f = node.g = node.h = 0;
                        node.parentNode = null;
                    }
                    break;

              
                case 2:
             
                    _gameState = Gamestates.Flocking;

                    for (int i = 0; i < flockingScript.PLAYERACTORS; i++)
                    {

                        flockingScript.player_actor[i].GetComponent<SimpleActor>().enabled = false;
                        flockingScript.player_actor[i].GetComponent<Flocking_SimpleActor>().enabled = true;
                        flockingScript.player_actor[i].GetComponent<FlockingController>().enabled = true;
                        flockingScript.player_actor[i].GetComponent<OrientedActor>().enabled = true;
                        flockingScript.player_actor[i].GetComponent<PlayerController>().enabled = false;
                    }


                    break;

                case 3:
                    _gameState = Gamestates.PathFinding;

                    for (int i = 0; i < 1; i++)
                    {

                        flockingScript.player_actor[0].GetComponent<SimpleActor>().enabled = true;
                        flockingScript.player_actor[0].GetComponent<Flocking_SimpleActor>().enabled = false;
                        flockingScript.player_actor[0].GetComponent<FlockingController>().enabled = false;
                        flockingScript.player_actor[0].GetComponent<OrientedActor>().enabled = false;
                       flockingScript.player_actor[0].GetComponent<PlayerController>().enabled = true;
                    }

                    for (int i = 1; i < flockingScript.PLAYERACTORS; i++)
                    {
                        Destroy(flockingScript.player_actor[i].gameObject);
                    }



                        for (int i = 0; i < flockingScript.TOTALACTORS; i++)
                    {

                                flockingScript.actors[i].GetComponent<SimplePathFollowingActor>().enabled = true;
                                flockingScript.actors[i].GetComponent<Pathfinding>().enabled = true;
                                flockingScript.actors[i].GetComponent<SeekAndArriveController>().enabled = true;
              
                    }
                    FindUnblockedNodes();
                    for (int i = 0; i < flockingScript.TOTALACTORS; i++)
                    {
                        //if (!flockingScript.actors[i].GetComponent<StartingPositionController>().enabled)
                        {
                            AssignStartAndEndNodes(i);
                        }
                    }
                    for (int i = 0; i < flockingScript.TOTALACTORS; i++)
                    {
                       if (!flockingScript.actors[i].GetComponent<StartingPositionController>().enabled && flockingScript.actors[i].GetComponent<Pathfinding>().enabled == true)
                        {
                          //  if (!isGoalNodeGiven)
                            {
                                SetActorPositions(i);
                            }
                        }
                    }

                    for (int i = 0; i < flockingScript.TOTALACTORS; i++)
                    {
                        if (!flockingScript.actors[i].GetComponent<StartingPositionController>().enabled)
                        {
                            FindPath(i);
                        }
                    }

                    for (int i = 0; i < flockingScript.TOTALACTORS; i++)
                    {
                        if (!flockingScript.actors[i].GetComponent<StartingPositionController>().enabled)
                        {
                            flockingScript.SActor[i].currentPoint = 0;
                            flockingScript.SActor[i].isPathCalculated = true;
                        }
                    }
                    break;
               

                default:
                    _gameState = Gamestates.Start;
                    break;
            }
        }

     
       

        private void FindUnblockedNodes()
        {
            unblockedNodes.Clear();
            foreach (GridNode node in grid.nodes)
            {
                if (!node.blocked)
                {
                    unblockedNodes.Add(node);
                }
            }
        }

        private void AssignStartAndEndNodes(int i)
        {
            int temp = Random.Range(0, unblockedNodes.Count);
          
            if (!flockingScript.PFScripts[i].newStartNodeAssigned)
            {
                if (endNodes[i] == null)
                {
                    while (true)
                    {
                        if (!unblockedNodes[temp].isStartNode)
                        {
                            startNodes[i] = unblockedNodes[temp];
                            startNodes[i].isStartNode = true;
                            flockingScript.PFScripts[i].newStartNodeAssigned = true;
                            break;
                        }
                        else
                        {
                            temp = Random.Range(0, unblockedNodes.Count);
                        }
                    }
                }

                else
                {
                    startNodes[i] = endNodes[i];
                    endNodes[i].isEndNode = false;
                    startNodes[i].isStartNode = true;
                    flockingScript.PFScripts[i].newStartNodeAssigned = true;

                }
            }
            temp = Random.Range(0, unblockedNodes.Count);
            while (true)
            {
                if (!unblockedNodes[temp].isEndNode && (unblockedNodes[temp] != startNodes[i]))
                {
                    endNodes[i] = unblockedNodes[temp];
                    endNodes[i].isEndNode = true;
                    break;
                }
                else
                {
                    temp = Random.Range(0, unblockedNodes.Count);
                }
            }
        }

        private void SetActorPositions(int i)
        {
          

            flockingScript.actors[i].transform.position = startNodes[i].transform.position;
            flockingScript.actors[i].transform.rotation = Quaternion.identity;
            flockingScript.PFScripts[i].AssignStartEndNode(startNodes[i], endNodes[i]);
        }

        private void FindPath(int i)
        {
            while (flockingScript.PFScripts[i].pathNotFound)
            {
                foreach (GridNode node in grid.nodes)
                {
                    node.f = node.g = node.h = 0;
                    node.parentNode = null;
                }
                flockingScript.PFScripts[i].Recalculate_AStar();
                if (!flockingScript.PFScripts[i].pathNotFound)
                {
                    TracePath(i);
                    pathPoints[i].Reverse();
                    flockingScript.SActor[i].pathpoints = pathPoints[i].ToArray();
                    flockingScript.SActor[i].DrawPath();
                    flockingScript.SActor[i].totalPathPoints = flockingScript.SActor[i].pathpoints.Length;
                    flockingScript.SActor[i].startPoint = pathPoints[i][0];
                    flockingScript.SActor[i].endPoint = flockingScript.SActor[i].pathpoints[flockingScript.SActor[i].totalPathPoints - 1];
                    pathPointsTerrainTypes[i].Reverse();
                    flockingScript.SActor[i].pathpointsTerrains = pathPointsTerrainTypes[i].ToArray();
                }
                else
                {
                    flockingScript.PFScripts[i].openList.Clear();
                    flockingScript.PFScripts[i].closedList.Clear();
                    AssignStartAndEndNodes(i);
                    SetActorPositions(i);
                }
            }
        }
        private void TracePath(int i)
        {
            GridNode endNode = endNodes[i];           
            pathPoints[i].Clear();
            pathPointsTerrainTypes[i].Clear();
            while (endNode.parentNode != null)
            {
                pathPoints[i].Add(endNode.transform.position);
                pathPointsTerrainTypes[i].Add(endNode.terrainType);
                endNode = endNode.parentNode;
            }
            pathPoints[i].Add(startNodes[i].transform.position);
            pathPointsTerrainTypes[i].Add(startNodes[i].terrainType);
            int pointsCount = pathPoints[i].Count;
            for (int j = 0; j < pointsCount; j++)
            {
                pathPoints[i][j] = new Vector3(pathPoints[i][j].x, pathPoints[i][j].y, -1.0f);
            }
        }

        private void Update()
        {
            //for(int i =0; i<flockingScript.TOTALACTORS;i++)
            {
                // Debug.Log(flockingScript.actors.Length);
                if (flockingScript.Total == 0)
                {
                    _text.text = "You Win";
                }
            }
              
        }

    }
}