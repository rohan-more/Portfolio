using UnityEngine;

namespace AISandbox
{
    public class DrawingWalls : State
    {
        private const string _name = "DrawingWalls";
        public override string Name
        {
            get
            {
                return _name;
            }
        }
        private bool _draw_blocked;
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
                }
            }
        }
    }
}
