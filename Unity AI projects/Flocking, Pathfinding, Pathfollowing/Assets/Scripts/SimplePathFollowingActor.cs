using UnityEngine;
using System.Collections;

namespace AISandbox
{
    public class SimplePathFollowingActor : MonoBehaviour, IActor
    {
        private const float ROADSPEED = 5.0f;
        private const float FORESTSPEED = 2.0f;
        private const float WATERSPEED = 1.5f;
        private const float STEERING_ACCEL = 100.0f;
        private const float VELOCITY_LINE_SCALE = 0.1f;
        private const float STEERING_LINE_SCALE = 4.0f;
        private const float SLOWSPEED = 0.5f;

        private Vector2 initialVelocity = Vector2.zero;
        [HideInInspector]
        public Vector3[] pathpoints;
        [HideInInspector]
        public int totalPathPoints;
        [HideInInspector]
        public Vector3 startPoint;
        [HideInInspector]
        public Vector3 endPoint;
        [HideInInspector]
        public int currentPoint = 1;
        [HideInInspector]
        public GridNode.TerrainType[] pathpointsTerrains;

        public GridNode.TerrainType currentTerrainType;
        

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

        [HideInInspector]
        public int rollNumber;

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

        private Vector2 _steering = Vector2.zero;
        private Vector2 _acceleration = Vector2.zero;
        public Vector2 _velocity = Vector2.zero;

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
        public void SetPlayerInput(float x_axis, float y_axis)
        {
            
        }
        public void SetFlockingInput(float x_axis, float y_axis)
        {
            
        }
        public void DrawPath()
        {
            _path_tracer.SetVertexCount(pathpoints.Length);
            _path_tracer.SetPositions(pathpoints);

        }

        public float MaxSpeed
        {
            get
            {
                if (currentTerrainType == GridNode.TerrainType.Road)
                {
                    return ROADSPEED;
                }                 
                else if (currentTerrainType == GridNode.TerrainType.Forest)
                {
                    return FORESTSPEED;
                }
                else
                {
                    return WATERSPEED;
                }
                
            }

        }

       

        public Vector2 Velocity
        {
            get { return _velocity; }
        }

        public void SetVelocity(Vector2 value)
        {
            _velocity = value;
        }
        public void SetAcceleration(Vector2 value)
        {
            _acceleration = value;
        }
        public void SetSteering(Vector2 value)
        {
            _steering = value;
        }
        private void FixedUpdate()
        {
            _velocity += _acceleration * Time.fixedDeltaTime;
            float distance = Vector2.Distance(ToVector2(endPoint), ToVector2(transform.position));
            if (distance < SLOWSPEED)
            {
                
                _velocity = Vector2.ClampMagnitude(_velocity, MaxSpeed * (distance / SLOWSPEED));
            }
            else
            {
                //Debug.Log(MaxSpeed);
                _velocity = Vector2.ClampMagnitude(_velocity, MaxSpeed);
            }
            Vector3 position = transform.position;
            position += (Vector3)(_velocity * Time.fixedDeltaTime);
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.Normalize(_velocity));
        }

        private void Update()
        {
            currentTerrainType = pathpointsTerrains[currentPoint];
            _steering_line.transform.rotation = Quaternion.identity;
            _steering_line.SetPosition(1, _steering * STEERING_LINE_SCALE);
           // _path_tracer.SetVertexCount(pathpoints.Length);
            //_path_tracer.SetPosition(0, startPoint);
            /*for (int i = 1; i < pathpoints.Length; i++)
            {
                _path_tracer.SetPosition(i, pathpoints[i]);
            }*/
            // The steering is reset every frame so SetInput() must be called every frame for continuous steering.
            _steering = Vector2.zero;
            _acceleration = Vector2.zero;
         //   _velocity = Vector2.zero;
        }
        private Vector2 ToVector2(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }
    }
}