using UnityEngine;
using System.Collections;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class SeekAndArriveController : MonoBehaviour
    {
        private Vector2 steering = Vector2.zero;
        private IActor _actor;

        private GameObjectButtons gbObject;
        private void Awake()
        {
            _actor = GetComponent<IActor>();
            gbObject = GameObject.Find("Canvas2").GetComponent<GameObjectButtons>();
        }

        private void FixedUpdate()
        {
            if (GameManager.gameManager.simpleActorScript.isPathCalculated)
            {
                steering = (new Vector2(GameManager.gameManager.simpleActorScript.pathpoints[GameManager.gameManager.simpleActorScript.currentPoint].x - transform.position.x, GameManager.gameManager.simpleActorScript.pathpoints[GameManager.gameManager.simpleActorScript.currentPoint].y - transform.position.y)).normalized;

                if (Vector2.Distance(GameManager.gameManager.simpleActorScript.pathpoints[GameManager.gameManager.simpleActorScript.currentPoint], transform.position) < 0.01f)
                {
                    if (GameManager.gameManager.simpleActorScript.currentPoint <= GameManager.gameManager.simpleActorScript.pathpoints.Length - 2)
                    {
                        GameManager.gameManager.simpleActorScript.currentPoint++;
                    }
                }
                if (Vector2.Distance(GameManager.gameManager.simpleActorScript.endPoint, transform.position) < 0.01f)
                {
                    GameManager.gameManager.simpleActorScript.isApproachingTarget = false;
                    BitManipulator.SetBit(ref GameManager.gameManager.controlbits, GameManager.gameManager.specialNodeNumber);
                    switch (GameManager.gameManager.specialNodeNumber)
                    {
                        case 0:
                        case 1:
                        case 2:
                            AddToInventory(GameManager.gameManager.endNode.gridNodeType);
                            break;
                        case 3:
                        case 4:

                        case 5:
                            GameManager.gameManager.endNode.myRenderer.sprite = GameManager.gameManager.unlockedDoor;
                            break;
                        case 6:
                            AddToInventory(GameManager.gameManager.endNode.gridNodeType);
                            if (!EntryPoint.doNotRecordAnymoreMessages)
                            {
                                EntryPoint.messages.Add((EntryPoint.messages.Count).ToString() + ": Treasure collected");
                                CanvasManager.message.text = "Mission Successful";
                                EntryPoint.doNotRecordAnymoreMessages = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            // Pass all parameters to the character control script.
            _actor.SetInput(steering.x, steering.y);
        }
        public void AddToInventory(GridNode.GridNodeType gridNodeType)
        {
            if (gridNodeType != GridNode.GridNodeType.Normal)
            {
                foreach (DragHandler dragHandlerScript in CanvasManager.dragHandlerScripts)
                {
                    if (dragHandlerScript.itemType == gridNodeType)
                    {
                        dragHandlerScript.gameObject.SetActive(true);
                        dragHandlerScript.gameObject.transform.position = dragHandlerScript.startPosition;
                    }
                }
                GridNode node = GameManager.gameManager.grid.specialNodes[gridNodeType];
                if (node.gridNodeType == GridNode.GridNodeType.GreenKey)
                {
                    gbObject.GreenKey.gameObject.SetActive(true);
                }
                else if (node.gridNodeType == GridNode.GridNodeType.RedKey)
                {
                    gbObject.RedKey.gameObject.SetActive(true);
                }
                else if (node.gridNodeType == GridNode.GridNodeType.BlueKey)
                {
                    gbObject.BlueKey.gameObject.SetActive(true);
                }
                else
                {
                    gbObject.Treasure.gameObject.SetActive(true);
                }

                node.gridNodeType = GridNode.GridNodeType.Normal;

               // CanvasManager.notification.text = "" + gridNodeType.ToString() + " picked up!";


            }
        }
    }

}