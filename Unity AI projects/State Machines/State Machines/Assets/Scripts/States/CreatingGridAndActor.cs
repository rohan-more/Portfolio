using UnityEngine;

namespace AISandbox
{
    public class CreatingGridAndActor : State
    {
        private const string _name = "CreatingGridAndActor";
        public override string Name
        {
            get
            {
                return _name;
            }
        }
        private static readonly int NUMBER_OF_ROWS = 20;
        private static readonly int NUMBER_OF_COLUMNS = 40;

       
        public static Color _waterColor;
        public static Color _forestColor;
        public static Color _roadColor;
        public static Color _blockedColor;
        public static Sprite unlockedDoor;
        public static Grid grid;
        private static GameObject actorPrefab;

        public static GameObject actor;
        public static Pathfinding pathfindingScript;
        public static SimpleActor simpleActorScript;
        public static GridNode startNode;
        public static GridNode endNode;
        public override void Enter()
        {
            base.Enter();                  
           

            _waterColor = new Color(0.0f, 0.0f, 255.0f);
            _forestColor = new Color(0.0f, 128.0f, 0.0f);
            _roadColor = new Color(0.501f, 0.501f, 0.501f);
            _blockedColor = new Color(0.647f, 0.164f, 0.164f);
            unlockedDoor = Resources.Load<Sprite>("Sprites/door_open");

            grid = GameObject.Find("Grid").GetComponent<Grid>();
            CreateGrid(NUMBER_OF_ROWS, NUMBER_OF_COLUMNS);
            grid.transform.position = new Vector3(grid.size.x * -0.5f, grid.size.y * 0.5f);

            actorPrefab= Resources.Load<GameObject>("Prefabs/SimpleActor");
            CreateActor();
            EntryPoint.stateMachine.SetActiveState("PlacingGameObjects");
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
                // node.myRenderer.sprite = forest;
                node.myRenderer.color = _forestColor;
            }
            else
            {
                //node.myRenderer.sprite = water;
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
