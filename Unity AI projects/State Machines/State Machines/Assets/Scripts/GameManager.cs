using System.Collections.Generic;
using UnityEngine;
namespace AISandbox
{
    public class GameManager
    {
        private static GameManager _gameManager;
        private GameObjectButtons gbObject = GameObject.Find("Canvas").GetComponent<GameObjectButtons>();
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
        private GameManager() { }
        public bool AreAllObjectsInScene()
        {
            bool result = true;
            /* foreach (DragHandler dragHandlerScript in CanvasManager.dragHandlerScripts)

             {
                 result = result && !dragHandlerScript.enabled;
                 if (!result)
                 {
                     return result;
                 }
             }*/

            result = result && !gbObject.placeBlueDoor && !gbObject.placeRedDoor && !gbObject.placeGreenDoor
                            && !gbObject.placeBlueKey && !gbObject.placeRedKey && !gbObject.placeGreenKey
                            && !gbObject.placeTreasure;
            return result;
        }
        public void ResetGameManager()
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
                isANewDoorOpened = true;
                areAllKeysCollected = false;
                isANewkeyCollected = false;
                areAllDoorsOpened = false;
                Pathfollowing.changeStartPoint = false;
                CreatingGridAndActor.simpleActorScript.isPathCalculated = false;
                CreatingGridAndActor.startNode.isStartNode = false;
                CreatingGridAndActor.startNode.isEndNode = false;
                CreatingGridAndActor.endNode.isStartNode = false;
                CreatingGridAndActor.endNode.isEndNode = false;
                CreatingGridAndActor.startNode = CreatingGridAndActor.endNode = null;
                if (CreatingGridAndActor.pathfindingScript)
                {
                    CreatingGridAndActor.pathfindingScript.pathNotFound = true;
                    CreatingGridAndActor.pathfindingScript.openList.Clear();
                    CreatingGridAndActor.pathfindingScript.closedList.Clear();
                }
                if (CreatingGridAndActor.actor)
                {
                    CreatingGridAndActor.actor.SetActive(false);
                }
                if (CreatingGridAndActor.simpleActorScript)
                {
                    CreatingGridAndActor.simpleActorScript.currentPoint = 1;
                }

                foreach (GridNode node in CreatingGridAndActor.grid.nodes)
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
                foreach (KeyValuePair<GridNode.GridNodeType, GridNode> node in CreatingGridAndActor.grid.specialNodes)
                {
                    node.Value.gridNodeType = GridNode.GridNodeType.Normal;
                    node.Value.myRenderer.color = PlacingGameObjects.originalColor;
                }
                CreatingGridAndActor.grid.specialNodes.Clear();
            }
        }
    }
}

