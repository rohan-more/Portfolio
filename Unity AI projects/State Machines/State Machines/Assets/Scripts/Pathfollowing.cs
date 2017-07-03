using UnityEngine;
using System.Collections.Generic;

namespace AISandbox
{
    public static class Pathfollowing
    {
        public static bool changeStartPoint;

        public static List<GridNode> unblockedNodes = new List<GridNode>();

        private static List<Vector3> pathPoints = new List<Vector3>();
        private static List<GridNode.TerrainType> pathPointsTerrainTypes = new List<GridNode.TerrainType>();
        private static List<GridNode.GridNodeType> pathPointsGridNodeTypes = new List<GridNode.GridNodeType>();

        public static void FindUnblockedNodes()
        {
            if (GameManager.gameManager.isNodeFindingNeeded)
            {
                unblockedNodes.Clear();
                foreach (GridNode node in CreatingGridAndActor.grid.nodes)
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
            GridNode node = CreatingGridAndActor.grid.specialNodes[(GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber];
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
            if (CreatingGridAndActor.endNode == null && GameManager.gameManager.controlbits == 0)
            {
                for (int i = 0; i < unblockedNodes.Count; i++)
                {
                    if (!unblockedNodes[i].isStartNode)
                    {
                        CreatingGridAndActor.startNode = unblockedNodes[i];
                        CreatingGridAndActor.startNode.isStartNode = true;
                        break;
                    }
                }
            }
            else if(changeStartPoint)
            {
                CreatingGridAndActor.startNode.isStartNode = false;
                CreatingGridAndActor.startNode = CreatingGridAndActor.endNode;
                CreatingGridAndActor.endNode.isEndNode = false;
                CreatingGridAndActor.startNode.isStartNode = true;
            }
            CreatingGridAndActor.endNode = CreatingGridAndActor.grid.specialNodes[(GridNode.GridNodeType)GameManager.gameManager.specialNodeNumber];
            CreatingGridAndActor.endNode.isEndNode = true;
        }

        public static void SetActorPositions()
        {
            CreatingGridAndActor.pathfindingScript.openList.Clear();
            CreatingGridAndActor.pathfindingScript.closedList.Clear();
            CreatingGridAndActor.actor.SetActive(true);
            CreatingGridAndActor.actor.transform.position = CreatingGridAndActor.startNode.transform.position;
            CreatingGridAndActor.actor.transform.rotation = Quaternion.identity;
            CreatingGridAndActor.pathfindingScript.AssignStartEndNode(CreatingGridAndActor.startNode, CreatingGridAndActor.endNode);
        }

        public static bool StartPathFinding()
        {
            while (CreatingGridAndActor.pathfindingScript.pathNotFound)
            {
                foreach (GridNode node in CreatingGridAndActor.grid.nodes)
                {
                    node.f = node.g = node.h = 0;
                    node.parentNode = null;
                }              
                CreatingGridAndActor.pathfindingScript.Recalculate_AStar();
                if (!CreatingGridAndActor.pathfindingScript.pathNotFound)
                {
                    TracePath();
                    pathPoints.Reverse();
                    CreatingGridAndActor.simpleActorScript.pathpoints = pathPoints.ToArray();
                    CreatingGridAndActor.simpleActorScript.DrawPath();
                    CreatingGridAndActor.simpleActorScript.endPoint = CreatingGridAndActor.simpleActorScript.pathpoints[CreatingGridAndActor.simpleActorScript.pathpoints.Length - 1];
                    pathPointsTerrainTypes.Reverse();
                    CreatingGridAndActor.simpleActorScript.pathpointsTerrains = pathPointsTerrainTypes.ToArray();
                    pathPointsGridNodeTypes.Reverse();
                    CreatingGridAndActor.simpleActorScript.pathpointsGridNodes = pathPointsGridNodeTypes.ToArray();
                    changeStartPoint = true;
                    return true;
                }
                else
                {                   
                    changeStartPoint = false;
                    return false;
                }
            }
            return false;
        }
        private static void TracePath()
        {
            GridNode endNode = CreatingGridAndActor.endNode;
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
            pathPoints.Add(CreatingGridAndActor.startNode.transform.position);
            pathPointsTerrainTypes.Add(CreatingGridAndActor.startNode.terrainType);
            pathPointsGridNodeTypes.Add(CreatingGridAndActor.startNode.gridNodeType);
            int pointsCount = pathPoints.Count;
            for (int i = 0; i < pointsCount; i++)
            {
                pathPoints[i] = new Vector3(pathPoints[i].x, pathPoints[i].y, -1.0f);
            }
        }
    }
}