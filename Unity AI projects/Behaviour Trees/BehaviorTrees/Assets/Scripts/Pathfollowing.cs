using UnityEngine;
using System.Collections.Generic;

namespace AISandbox
{
    public static class Pathfollowing
    {
        public static List<GridNode> unblockedNodes = new List<GridNode>();

        private static List<Vector3> pathPoints = new List<Vector3>();
        private static List<GridNode.TerrainType> pathPointsTerrainTypes = new List<GridNode.TerrainType>();
        private static List<GridNode.GridNodeType> pathPointsGridNodeTypes = new List<GridNode.GridNodeType>();

        public static void FindUnblockedNodes()
        {
            if (GameManager.gameManager.isNodeFindingNeeded)
            {
                unblockedNodes.Clear();
                foreach (GridNode node in GameManager.gameManager.grid.nodes)
                {
                    if (!node.blocked)
                    {
                        unblockedNodes.Add(node);
                    }
                }
                GameManager.gameManager.isNodeFindingNeeded = false;
            }
        }
        public static void AddTheDoorNodeToTheUnblockedList()
        {
            GridNode node = GameManager.gameManager.grid.specialNodes[(GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber];
            if (node.blocked)
            {
                Sprite temp = node.myRenderer.sprite;
                node.blocked = false;
                node.myRenderer.sprite = temp;
                unblockedNodes.Add(node);
            }
        }

        public static void AssignStartAndEndNodes()
        {
            if (GameManager.gameManager.endNode == null && GameManager.gameManager.controlbits == 0)
            {
                for (int i = 0; i < unblockedNodes.Count; i++)
                {
                    if (!unblockedNodes[i].isStartNode)
                    {
                        GameManager.gameManager.startNode = unblockedNodes[i];
                        GameManager.gameManager.startNode.isStartNode = true;
                        break;
                    }
                }
            }
            else if(GameManager.gameManager.changeStartPoint)
            {
                GameManager.gameManager.startNode.isStartNode = false;
                GameManager.gameManager.startNode = GameManager.gameManager.endNode;
                GameManager.gameManager.endNode.isEndNode = false;
                GameManager.gameManager.startNode.isStartNode = true;
            }
            GameManager.gameManager.endNode = GameManager.gameManager.grid.specialNodes[(GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber];
            GameManager.gameManager.endNode.isEndNode = true;
        }

        public static void SetActorPositions()
        {
            GameManager.gameManager.pathfindingScript.openList.Clear();
            GameManager.gameManager.pathfindingScript.closedList.Clear();
            GameManager.gameManager.actor.SetActive(true);
            GameManager.gameManager.actor.transform.position = GameManager.gameManager.startNode.transform.position;
            GameManager.gameManager.actor.transform.rotation = Quaternion.identity;
            GameManager.gameManager.pathfindingScript.AssignStartEndNode(GameManager.gameManager.startNode, GameManager.gameManager.endNode);
        }

        public static bool StartPathFinding()
        {
            while (GameManager.gameManager.pathfindingScript.pathNotFound)
            {
                foreach (GridNode node in GameManager.gameManager.grid.nodes)
                {
                    node.f = node.g = node.h = 0;
                    node.parentNode = null;
                }
                GameManager.gameManager.pathfindingScript.Recalculate_AStar();
                if (!GameManager.gameManager.pathfindingScript.pathNotFound)
                {
                    TracePath();
                    pathPoints.Reverse();
                    GameManager.gameManager.simpleActorScript.pathpoints = pathPoints.ToArray();
                    GameManager.gameManager.simpleActorScript.DrawPath();
                    GameManager.gameManager.simpleActorScript.endPoint = GameManager.gameManager.simpleActorScript.pathpoints[GameManager.gameManager.simpleActorScript.pathpoints.Length - 1];
                    pathPointsTerrainTypes.Reverse();
                    GameManager.gameManager.simpleActorScript.pathpointsTerrains = pathPointsTerrainTypes.ToArray();
                    pathPointsGridNodeTypes.Reverse();
                    GameManager.gameManager.simpleActorScript.pathpointsGridNodes = pathPointsGridNodeTypes.ToArray();
                    GameManager.gameManager.changeStartPoint = true;
                    return true;
                }
                else
                {                   
                    GameManager.gameManager.changeStartPoint = false;
                    return false;
                }
            }
            return false;
        }
        private static void TracePath()
        {
            GridNode endNode = GameManager.gameManager.endNode;
            pathPoints.Clear();
            pathPointsTerrainTypes.Clear();
            pathPointsGridNodeTypes.Clear();
            while (endNode.parentNode != null)
            {
                pathPoints.Add(endNode.transform.position);
                pathPointsTerrainTypes.Add(endNode.terrainType);
                pathPointsGridNodeTypes.Add(endNode.gridNodeType);
                endNode = endNode.parentNode;
            }
            pathPoints.Add(GameManager.gameManager.startNode.transform.position);
            pathPointsTerrainTypes.Add(GameManager.gameManager.startNode.terrainType);
            pathPointsGridNodeTypes.Add(GameManager.gameManager.startNode.gridNodeType);
            int pointsCount = pathPoints.Count;
            for (int i = 0; i < pointsCount; i++)
            {
                pathPoints[i] = new Vector3(pathPoints[i].x, pathPoints[i].y, -1.0f);
            }
        }
    }
}