using UnityEngine;
using System.Collections.Generic;

namespace AISandbox
{
    class GridManipulation : MonoBehaviour
    {
        public static readonly Color32 originalColor = new Color32(255, 255, 255, 192);
        private bool _draw_blocked;
        private GameObjectButtons gbScript;
        public void Start()
        {
            gbScript = GameObject.Find("Canvas2").GetComponent<GameObjectButtons>();
        }
        private void Update()
        {
            if (((GameManager.gameManager.gamestate == GameManager.GameStates.DrawingWalls) ||
                (GameManager.gameManager.gamestate == GameManager.GameStates.PlacingGameObjects)) &&
                Input.GetMouseButton(0))
            {
                Vector3 world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 local_pos = GameManager.gameManager.grid.transform.InverseTransformPoint(world_pos);
                // This trick makes a lot of assumptions that the nodes haven't been modified since initialization.
                int column = Mathf.FloorToInt(local_pos.x / GameManager.gameManager.grid.node_width);
                int row = Mathf.FloorToInt(-local_pos.y / GameManager.gameManager.grid.node_height);
                if (row >= 0 && row < GameManager.gameManager.grid.nodes.GetLength(0) && column >= 0 && column < GameManager.gameManager.grid.nodes.GetLength(1))
                {
                    GridNode node = GameManager.gameManager.grid.nodes[row, column];
                    switch (GameManager.gameManager.gamestate)
                    {
                        case GameManager.GameStates.DrawingWalls:
                            if (node.gridNodeType == GridNode.GridNodeType.Normal)
                            {
                                
                                if (Input.GetMouseButtonDown(0))
                                {
                                    _draw_blocked = !node.blocked;
                                }
                                if (node.blocked != _draw_blocked)
                                {
                                    node.blocked = _draw_blocked;
                                }
                            }
                            break;

                        case GameManager.GameStates.PlacingGameObjects:
                            
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
                                GameManager.gameManager.grid.specialNode = new KeyValuePair<GridNode.GridNodeType, GridNode>(node.gridNodeType, node);



                            }
                            break;
                    }
                    
                    }
                }
            }
        }
    }