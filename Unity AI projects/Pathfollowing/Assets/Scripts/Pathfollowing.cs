using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace AISandbox
{
    public class Pathfollowing : MonoBehaviour
    {
        private const int TOTALACTORS = 10;

        private const int NUMBER_OF_ROWS = 20;
        private const int NUMBER_OF_COLUMNS = 20;

        private GridNode[] startNodes;
        private GridNode[] endNodes;

        private GameObject[] actors;
        private GameObject actorPrefab;
        private Pathfinding[] PFScripts;
        private SimpleActor[] SActor;

        private Dictionary<int, List<Vector3>> pathPoints;
        private Dictionary<int, List<GridNode.TerrainType>> pathPointsTerrainTypes;
        private List<GridNode> unblockedNodes;

        private Grid grid;

        private Dropdown _menuDropdown;
        private Toggle _debugToggle;
        

        public enum Gamestates { DrawingWalls, PathFinding }
        private Gamestates _gameState = Gamestates.DrawingWalls;
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
            Vector2 gridSize = grid.size;
            Vector2 gridPos = new Vector2(gridSize.x * -0.5f, gridSize.y * 0.5f);
            grid.transform.position = gridPos;

            startNodes = new GridNode[TOTALACTORS];
            endNodes = new GridNode[TOTALACTORS];

            pathPoints = new Dictionary<int, List<Vector3>>(TOTALACTORS);
            pathPointsTerrainTypes = new Dictionary<int, List<GridNode.TerrainType>>(TOTALACTORS);
            for (int i = 0; i < TOTALACTORS; i++)
            {
                pathPoints[i] = new List<Vector3>();
                pathPointsTerrainTypes[i] = new List<GridNode.TerrainType>();
            }       
            unblockedNodes = new List<GridNode>();

            actorPrefab = Resources.Load<GameObject>("Prefabs/SimpleActor");

            CreateActors();
            GameObject canvas = GameObject.Find("Canvas");
            _menuDropdown = canvas.GetComponent<Transform>().FindChild("MenuDropdown").GetComponent<Dropdown>();
            _debugToggle = canvas.GetComponent<Transform>().FindChild("DebugToggle").GetComponent<Toggle>();

            _menuDropdown.onValueChanged.AddListener(OnDropdownMenuValueChanged);
            _debugToggle.onValueChanged.AddListener(OnDebugToggleValueChanged);
            
           
        }

        private void CreateActors()
        {
            actors = new GameObject[TOTALACTORS];
            PFScripts = new Pathfinding[TOTALACTORS];
            SActor = new SimpleActor[TOTALACTORS];
            for (int i = 0; i < TOTALACTORS; i++)
            {
                actors[i] = Instantiate(actorPrefab, transform) as GameObject;
                actors[i].name = "Actor_" + (i + 1);
                PFScripts[i] = actors[i].GetComponent<Pathfinding>();
                SActor[i] = actors[i].GetComponent<SimpleActor>();
                SActor[i].rollNumber = i;
                actors[i].SetActive(false);
            }
        }


        private void OnDebugToggleValueChanged(bool DebugModeOn)
        {
            int actor = 2;
            if (DebugModeOn)
            {
                SActor[(int)actor - 1].DrawDebugData = true;
            }
            else
            {
                
                for (int i = 0; i < TOTALACTORS; i++)
                {
                    SActor[i].DrawDebugData = false;
                }
            }
        }

        public void RecalculatePath(int i)
        {
            PFScripts[i].pathNotFound = true;
            AssignStartAndEndNodes(i);
            SetActorPositions(i);
            FindPath(i);
            SActor[i].currentPoint = 1;
            SActor[i].isPathCalculated = true;
        }



        private void OnDropdownMenuValueChanged(int option)
        {
            switch (option)
            {
                case 0:
                    _gameState = Gamestates.DrawingWalls;
                    for (int i = 0; i < TOTALACTORS; i++)
                    {
                        if (PFScripts[i])
                        {
                            PFScripts[i].pathNotFound = true;
                            PFScripts[i].openList.Clear();
                            PFScripts[i].closedList.Clear();
                        }
                        if (actors[i])
                        {
                            actors[i].SetActive(false);
                        }
                        if (SActor[i])
                        {
                            SActor[i].currentPoint = 0;
                        }
                    }
                    foreach (GridNode node in grid.nodes)
                    {
                        node.f = node.g = node.h = 0;
                        node.parentNode = null;
                    }
                    break;
                case 1:
                    _gameState = Gamestates.PathFinding;
                    FindUnblockedNodes();
                    for (int i = 0; i < TOTALACTORS; i++)
                    {
                        AssignStartAndEndNodes(i);
                    }
                    for (int i = 0; i < TOTALACTORS; i++)
                    {
                        SetActorPositions(i);
                    }

                    for (int i = 0; i < TOTALACTORS; i++)
                    {
                        FindPath(i);
                    }

                    for (int i = 0; i < TOTALACTORS; i++)
                    {
                        SActor[i].currentPoint = 0;
                        SActor[i].isPathCalculated = true;
                    }
                    break;
                default:
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
            if (!PFScripts[i].newStartNodeAssigned)
            {
                if (endNodes[i] == null)
                {
                    while (true)
                    {
                        if (!unblockedNodes[temp].isStartNode)
                        {
                            startNodes[i] = unblockedNodes[temp];
                            startNodes[i].isStartNode = true;
                            PFScripts[i].newStartNodeAssigned = true;
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
                    PFScripts[i].newStartNodeAssigned = true;

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
            actors[i].SetActive(true);
            actors[i].transform.position = startNodes[i].transform.position;
            actors[i].transform.rotation = Quaternion.identity;
            PFScripts[i].AssignStartEndNode(startNodes[i], endNodes[i]);
        }

        private void FindPath(int i)
        {
            while (PFScripts[i].pathNotFound)
            {
                foreach (GridNode node in grid.nodes)
                {
                    node.f = node.g = node.h = 0;
                    node.parentNode = null;
                }
                PFScripts[i].Recalculate_AStar();
                if (!PFScripts[i].pathNotFound)
                {
                    TracePath(i);
                    pathPoints[i].Reverse();
                    SActor[i].pathpoints = pathPoints[i].ToArray();
                    SActor[i].DrawPath();
                    SActor[i].totalPathPoints = SActor[i].pathpoints.Length;
                    SActor[i].startPoint = pathPoints[i][0];
                    SActor[i].endPoint = SActor[i].pathpoints[SActor[i].totalPathPoints - 1];
                    pathPointsTerrainTypes[i].Reverse();
                    SActor[i].pathpointsTerrains = pathPointsTerrainTypes[i].ToArray();
                }
                else
                {
                    PFScripts[i].openList.Clear();
                    PFScripts[i].closedList.Clear();
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
    }
}