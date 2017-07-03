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
            gbObject = GameObject.Find("Canvas").GetComponent<GameObjectButtons>();
        }

        private void FixedUpdate()
        {
            if (CreatingGridAndActor.simpleActorScript.isPathCalculated)
            {
                steering = (new Vector2(CreatingGridAndActor.simpleActorScript.pathpoints[CreatingGridAndActor.simpleActorScript.currentPoint].x - transform.position.x, CreatingGridAndActor.simpleActorScript.pathpoints[CreatingGridAndActor.simpleActorScript.currentPoint].y - transform.position.y)).normalized;

                if (Vector2.Distance(CreatingGridAndActor.simpleActorScript.pathpoints[CreatingGridAndActor.simpleActorScript.currentPoint], transform.position) < 0.01f)
                {
                    if (CreatingGridAndActor.simpleActorScript.currentPoint <= CreatingGridAndActor.simpleActorScript.pathpoints.Length - 2)
                    {
                        CreatingGridAndActor.simpleActorScript.currentPoint++;
                    }
                }
                if (Vector2.Distance(CreatingGridAndActor.simpleActorScript.endPoint, transform.position) < 0.01f)
                {
                    CreatingGridAndActor.simpleActorScript.isPathCalculated = false;
                    switch (GameManager.gameManager.specialNodeNumber)
                    {
                        case 1:
                        case 2:
                        case 3:
                            AddToInventory(CreatingGridAndActor.endNode.gridNodeType);
                            break;                       
                        case 4:
                            CreatingGridAndActor.endNode.myRenderer.color = Color.red;
                            CreatingGridAndActor.endNode.myRenderer.sprite = CreatingGridAndActor.unlockedDoor;
                            break;
                        case 5:
                            CreatingGridAndActor.endNode.myRenderer.color = Color.green;
                            CreatingGridAndActor.endNode.myRenderer.sprite = CreatingGridAndActor.unlockedDoor;
                            break;
                        case 6:
                            CreatingGridAndActor.endNode.myRenderer.color = Color.blue;
                           
                            CreatingGridAndActor.endNode.myRenderer.sprite = CreatingGridAndActor.unlockedDoor;
                            break;
                        case 7:
                            AddToInventory(CreatingGridAndActor.endNode.gridNodeType);
                            CanvasManager.message.text = "Mission Successful";
                            CanvasManager.notification.text = "Treasure Found! You Win!";
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
                GridNode node = CreatingGridAndActor.grid.specialNodes[gridNodeType];
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

                CanvasManager.notification.text = "" + gridNodeType.ToString() + " picked up!";
            
                // node.myRenderer.color = PlacingGameObjects.originalColor;
            }
        }
    }
}

