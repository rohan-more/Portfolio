using UnityEngine;
using System.Collections;

namespace AISandbox {
    public class SimpleActor : MonoBehaviour, IPlayerActor
    {
        private float MAX_SPEED           = 20.0f;
        private float STEERING_ACCEL      = 80.0f;
        private const float VELOCITY_LINE_SCALE = 0.1f;
        private const float STEERING_LINE_SCALE = 4.0f;
        //private const float STEERING_LINE_SCALE = 4.0f;

        [SerializeField]
        private bool _DrawVectors = true;
        public bool DrawVectors {
            get {
                return _DrawVectors;
            }
            set {
                _DrawVectors = value;
               // _steering_line.gameObject.SetActive(_DrawVectors);
               // _velocity_line.gameObject.SetActive(_DrawVectors);
            }
        }
        public LineRenderer _steering_line;
        public LineRenderer _velocity_line;

        private Vector2 _steering = Vector2.zero;
        private Vector2 _acceleration = Vector2.zero;
        public Vector2 _velocity = Vector2.zero;

        public bool wrapScreen = true;
        public bool _screenWrapX = false;
        public bool _screenWrapY = false;
        private Renderer _renderer;

        public float MaxSpeed
        {
            get
            {
                return MAX_SPEED;
            }
            set
            {
                MAX_SPEED = value;
            }
        }


 
      public  Vector2 Velocity {
            get
            {
                return _velocity;
            }
        }

        [SerializeField]
        private bool _MaxSpeedBool = true;
        private bool MaxSpeedBool
        {

            get
            {
                return _MaxSpeedBool;
            }
            set
            {
                _MaxSpeedBool = value;
                
            }
        }
      
      
      

        private void Start() {
            DrawVectors = _DrawVectors;

            _renderer = GetComponent<Renderer>();
        }

        public void SetPlayerInput( float x_axis, float y_axis ) {
            _steering = Vector2.ClampMagnitude( new Vector2(x_axis, y_axis), 1.0f );
            _acceleration = _steering * STEERING_ACCEL;
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

        private void Update() {
           

            Vector3 position = ScreenWrap();
            _velocity += _acceleration * Time.fixedDeltaTime;
            _velocity = Vector2.ClampMagnitude(_velocity, MAX_SPEED);
             transform.position = position;
            position += (Vector3)(_velocity * Time.fixedDeltaTime);
            transform.position = position;

            transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.Normalize(_velocity));
            _acceleration = Vector2.zero;
            
        }

    }
}