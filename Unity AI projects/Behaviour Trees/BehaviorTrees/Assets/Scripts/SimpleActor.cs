using UnityEngine;
using System.Collections;

namespace AISandbox
{
    public class SimpleActor : MonoBehaviour, IActor
    {
        private const float MAX_SPEED_ROAD = 1.5f;
        private const float MAX_SPEED_FOREST = 1.0f;
        private const float MAX_SPEED_WATER = 0.5f;
        private const float STEERING_ACCEL = 100.0f;
        private const float VELOCITY_LINE_SCALE = 0.1f;
        private const float STEERING_LINE_SCALE = 4.0f;
        private const float SLOWING_RADIUS = 0.5f;

        private Vector2 initialVelocity = Vector2.zero;
        [HideInInspector]
        public Vector3[] pathpoints;
        [HideInInspector]
        public Vector3 endPoint;
        [HideInInspector]
        public int currentPoint = 1;
        [HideInInspector]
        public GridNode.TerrainType[] pathpointsTerrains;
        [HideInInspector]
        public GridNode.GridNodeType[] pathpointsGridNodes;

        private GridNode.TerrainType currentTerrainType;
        private GridNode.GridNodeType currentGridNodeType;

        private SeekAndArriveController seekAndArriveController;

        private bool _DrawDebugData = false;
        public bool DrawDebugData
        {
            get
            {
                return _DrawDebugData;
            }
            set
            {
                _DrawDebugData = value;
                _steering_line.gameObject.SetActive(_DrawDebugData);
                _path_tracer.enabled = (_DrawDebugData);
            }
        }

        public LineRenderer _steering_line;
        public LineRenderer _path_tracer;

        private bool _isPathCalculated = false;
        public bool isPathCalculated
        {
            get
            {
                return _isPathCalculated;
            }
            set
            {
                _isPathCalculated = value;
            }
        }
        [HideInInspector]
        public bool isApproachingTarget = false;
        private Vector2 _steering = Vector2.zero;
        private Vector2 _acceleration = Vector2.zero;
        private Vector2 _velocity = Vector2.zero;

        private void Start()
        {
            _velocity = initialVelocity;
            DrawDebugData = _DrawDebugData;
        }

        public void SetInput(float x_axis, float y_axis)
        {
            _steering = Vector2.ClampMagnitude(new Vector2(x_axis, y_axis), 1.0f);
            _acceleration = _steering * STEERING_ACCEL;
        }

        public float MaxSpeed
        {
            get
            {
                if (currentTerrainType == GridNode.TerrainType.Road)
                {
                    return MAX_SPEED_ROAD;
                }
                else if (currentTerrainType == GridNode.TerrainType.Forest)
                {
                    return MAX_SPEED_FOREST;
                }
                else
                {
                    return MAX_SPEED_WATER;
                }
            }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
        }

        private void Awake()
        {
            seekAndArriveController = GetComponent<SeekAndArriveController>();
        }

        private void FixedUpdate()
        {
            if (_isPathCalculated)
            {
                _velocity += _acceleration * Time.fixedDeltaTime;
                float distance = Vector2.Distance(endPoint, transform.position);
                if (distance < SLOWING_RADIUS)
                {
                    _velocity = Vector2.ClampMagnitude(_velocity, MaxSpeed * (distance / SLOWING_RADIUS));
                }
                else
                {
                    _velocity = Vector2.ClampMagnitude(_velocity, MaxSpeed);
                }
                Vector3 position = transform.position;
                position += (Vector3)(_velocity * Time.fixedDeltaTime);
                transform.position = position;
                transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.Normalize(_velocity));
            }
        }

        private void Update()
        {
            if (_isPathCalculated)
            {
                currentTerrainType = pathpointsTerrains[currentPoint - 1];
                currentGridNodeType = pathpointsGridNodes[currentPoint - 1];
                CheckForKeysOrDoorsOntheWay();
            }
            _steering_line.transform.rotation = Quaternion.identity;
            _steering_line.SetPosition(1, _steering * STEERING_LINE_SCALE);
            // The steering is reset every frame so SetInput() must be called every frame for continuous steering.
            _steering = Vector2.zero;
            _acceleration = Vector2.zero;
        }
        public void DrawPath()
        {
            _path_tracer.SetVertexCount(pathpoints.Length);
            _path_tracer.SetPositions(pathpoints);
        }
        private void CheckForKeysOrDoorsOntheWay()
        {
            if (currentGridNodeType != GridNode.GridNodeType.Normal)
            {
                if (currentGridNodeType == GridNode.GridNodeType.RedKey || currentGridNodeType == GridNode.GridNodeType.GreenKey || currentGridNodeType == GridNode.GridNodeType.BlueKey)
                {
                    BitManipulator.SetBit(ref GameManager.gameManager.controlbits, (byte)currentGridNodeType);
                    seekAndArriveController.AddToInventory(currentGridNodeType);
                }
                else
                {
                    if (currentGridNodeType == GridNode.GridNodeType.RedDoor)
                    {
                        if(BitManipulator.IsBitSet(GameManager.gameManager.controlbits,0))
                        {
                            UnblockAndUnlockDoor();
                        }
                    }
                    if (currentGridNodeType == GridNode.GridNodeType.GreenDoor)
                    {
                        if (BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 1))
                        {
                            UnblockAndUnlockDoor();
                        }
                    }
                    if (currentGridNodeType == GridNode.GridNodeType.BlueDoor)
                    {
                        if (BitManipulator.IsBitSet(GameManager.gameManager.controlbits, 2))
                        {
                            UnblockAndUnlockDoor();
                        }
                    }
                }
            }
        }
        private void UnblockAndUnlockDoor()
        {
            GridNode node = GameManager.gameManager.grid.specialNodes[currentGridNodeType];
            if (node.blocked)
            {
                Sprite temp = node.myRenderer.sprite;
                node.blocked = false;
                node.myRenderer.sprite = temp;
                Pathfollowing.unblockedNodes.Add(node);
            }
            node.myRenderer.sprite = GameManager.gameManager.unlockedDoor;
            BitManipulator.SetBit(ref GameManager.gameManager.controlbits, (byte)currentGridNodeType);
        }
    }
}