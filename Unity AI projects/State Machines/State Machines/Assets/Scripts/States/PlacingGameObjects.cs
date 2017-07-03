using UnityEngine;
using System.Collections.Generic;

namespace AISandbox
{
    public class PlacingGameObjects : State
    {
        private const string _name = "PlacingGameObjects";
        private GameObjectButtons gbScript;

        public void Start()
        {
        }
        public override string Name
        {
            get
            {
                return _name;
            }
        }
        public override void Enter()
        {

            gbScript = GameObject.Find("Canvas").GetComponent<GameObjectButtons>();
            base.Enter();
        }
        public static readonly Color32 originalColor = new Color32(255, 255, 255, 255);
        public override void Execute()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 local_pos = CreatingGridAndActor.grid.transform.InverseTransformPoint(world_pos);
                // This trick makes a lot of assumptions that the nodes haven't been modified since initialization.
                int column = Mathf.FloorToInt(local_pos.x / CreatingGridAndActor.grid.node_width);
                int row = Mathf.FloorToInt(-local_pos.y / CreatingGridAndActor.grid.node_height);
                if (row >= 0 && row < CreatingGridAndActor.grid.nodes.GetLength(0) && column >= 0 && column < CreatingGridAndActor.grid.nodes.GetLength(1))
                {
                    GridNode node = CreatingGridAndActor.grid.nodes[row, column];
                    if (EntryPoint.stateMachine.GetActiveStateName() == "PlacingGameObjects")
                    {
                        if (!node.blocked && node.gridNodeType == GridNode.GridNodeType.Normal)
                        {
                            if (gbScript.placeGreenKey == true)
                            {
                                node.gridNodeType = GridNode.GridNodeType.GreenKey;
                                node.myRenderer.sprite = node.KeySprite;
                                node.myRenderer.color = Color.green;
                                gbScript.placeGreenKey = false;
                                

                            }
                            if (gbScript.placeBlueKey == true)
                            {
                                node.gridNodeType = GridNode.GridNodeType.BlueKey;
                                node.myRenderer.sprite = node.KeySprite;
                                node.myRenderer.color = Color.blue;
                                gbScript.placeBlueKey = false;

                            }
                            if (gbScript.placeRedKey == true)
                            {
                                node.gridNodeType = GridNode.GridNodeType.RedKey;
                                node.myRenderer.sprite = node.KeySprite;
                                node.myRenderer.color = Color.red;
                                gbScript.placeRedKey = false;
                            }
                            if (gbScript.placeGreenDoor == true)
                            {
                                node.gridNodeType = GridNode.GridNodeType.GreenDoor;
                                node.myRenderer.sprite = node.DoorSprite;
                                node.myRenderer.color = Color.green;
                                gbScript.placeGreenDoor = false;

                            }
                            if (gbScript.placeRedDoor == true)
                            {
                                node.gridNodeType = GridNode.GridNodeType.RedDoor;
                                node.myRenderer.sprite = node.DoorSprite;
                                node.myRenderer.color = Color.red;
                                gbScript.placeRedDoor = false;

                            }
                            if (gbScript.placeBlueDoor == true)
                            {
                                node.gridNodeType = GridNode.GridNodeType.BlueDoor;
                                node.myRenderer.sprite = node.DoorSprite;
                                node.myRenderer.color = Color.blue;
                                gbScript.placeBlueDoor = false;

                            }
                            if (gbScript.placeTreasure == true)
                            {
                                node.gridNodeType = GridNode.GridNodeType.Treasure;
                                node.myRenderer.sprite = node.TreasureSprite;
                                node.myRenderer.color = Color.yellow;
                                gbScript.placeTreasure = false;

                            }
                            CreatingGridAndActor.grid.specialNode = new KeyValuePair<GridNode.GridNodeType, GridNode>(node.gridNodeType, node);

                            /* if (node.gridNodeType != GridNode.GridNodeType.Normal && !DragHandler.isDragging)
                             {
                                 foreach (DragHandler dragHandlerScript in CanvasManager.dragHandlerScripts)
                                 {
                                     if (dragHandlerScript.itemType == node.gridNodeType)
                                     {
                                         dragHandlerScript.gameObject.SetActive(true);
                                         dragHandlerScript.enabled = true;
                                         dragHandlerScript.gameObject.transform.position = dragHandlerScript.startPosition;
                                         dragHandlerScript.canvasGroup.blocksRaycasts = true;
                                     }
                                 }*/
                            /*if(!CreatingGridAndActor.grid.specialNodes.Remove(node.gridNodeType))
                            {
                                Debug.Log("Special Node not found");
                            }*/
                            //node.gridNodeType = GridNode.GridNodeType.Normal;

                        }
                    }//node.myRenderer.color = originalColor;
                }
            }
            }
        }
    }

