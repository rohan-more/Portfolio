using UnityEngine;
using System.Collections;
using System;

namespace AISandbox {
    public class Flocking_SimpleActor : MonoBehaviour, IFlockingActor {
        private const float MAX_SPEED           = 10.0f;
        private const float STEERING_ACCEL      = 50.0f;
        private const float VELOCITY_LINE_SCALE = 0.1f;
        private const float STEERING_LINE_SCALE = 4.0f;

        public Vector2 initialVelocity = Vector2.zero;
        public bool wrapScreen = false;
        private bool _screenWrapX = false;
        private bool _screenWrapY = false;
        private Renderer _renderer;
        [SerializeField]
        private bool _DrawVectors = true;
        public bool DrawVectors {
            get {
                return _DrawVectors;
            }
            set {
                _DrawVectors = value;
                //_steering_line.gameObject.SetActive(_DrawVectors);
//_velocity_line.gameObject.SetActive(_DrawVectors);
            }
        }
        public LineRenderer _steering_line;
        public LineRenderer _velocity_line;

        private Vector2 _steering = Vector2.zero;
        private Vector2 _acceleration = Vector2.zero;
        private Vector2 _velocity = Vector2.zero;
        private OrientedActor o_actor;
        private void Start() {
            _velocity = initialVelocity;
            DrawVectors = _DrawVectors;
            _renderer = GetComponent<Renderer>();
            o_actor = GetComponent<OrientedActor>();
        }
        
           

        public void SetFlockingInput(float x_axis, float y_axis)
        {
            o_actor.SetFlockingInput(x_axis, y_axis);
        }
    

        public float MaxSpeed {
            get { return MAX_SPEED; }
        }

      

        public Vector2 Velocity {
            get { return _velocity; }
        }
        private Vector3 ScreenWrap()
        {
            Vector3 position = transform.position;
            if (wrapScreen)
            {
                if (_renderer.isVisible)
                {
                    _screenWrapX = false;
                    _screenWrapY = false;
                    return position;
                }
                else
                {
                    Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
                    if (!_screenWrapX && (viewportPosition.x > 1 || viewportPosition.x < 0))
                    {
                        position.x = -position.x;
                        _screenWrapX = true;
                    }
                    if (!_screenWrapY && (viewportPosition.y > 1 || viewportPosition.y < 0))
                    {
                        position.y = -position.y;
                        _screenWrapY = true;
                    }
                }
            }
            return position;
        }
        private void FixedUpdate() {
            Vector3 position = ScreenWrap();
            _velocity += _acceleration * Time.fixedDeltaTime;
            _velocity = Vector2.ClampMagnitude(_velocity, MAX_SPEED);
             position = transform.position;
            position += (Vector3)(_velocity * Time.fixedDeltaTime);
            transform.position = position;
        }

        private void Update() {
           // _steering_line.SetPosition( 1, _steering * STEERING_LINE_SCALE );
           // _velocity_line.SetPosition( 1, _velocity * VELOCITY_LINE_SCALE );
            // The steering is reset every frame so SetInput() must be called every frame for continuous steering.
            _steering = Vector2.zero;
            _acceleration = Vector2.zero;
        }

    }
}