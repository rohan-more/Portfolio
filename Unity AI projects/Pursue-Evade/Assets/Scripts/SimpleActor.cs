using UnityEngine;
using System.Collections;

namespace AISandbox
{
    public class SimpleActor : MonoBehaviour, IActor
    {
        private const float _MAX_SPEED = 40.0f;
        public float MAX_SPEED
        {
            get
            {
                return _MAX_SPEED;
            }
        }
        private const float STEERING_ACCEL = 100.0f;
        private const float VELOCITY_LINE_SCALE = 0.1f;
        private const float STEERING_LINE_SCALE = 4.0f;
        private const float CONSTANT_SPEED_MIN_VALUE = 5.0f;
        private float constantSpeed;

        [SerializeField]
        private bool _DrawVectors = true;
        public bool DrawVectors
        {
            get
            {
                return _DrawVectors;
            }
            set
            {
                _DrawVectors = value;
                _steering_line.gameObject.SetActive(_DrawVectors);
                _velocity_line.gameObject.SetActive(_DrawVectors);
            }
        }
        [SerializeField]
        private bool _IsSpeedConstant = false;
        public bool IsSpeedConstant
        {
            get
            {
                return _IsSpeedConstant;
            }
            set
            {
                _IsSpeedConstant = value;
            }
        }
        public LineRenderer _steering_line;
        public LineRenderer _velocity_line;

        private Vector2 _steering = Vector2.zero;
        private Vector2 _acceleration = Vector2.zero;
        private Vector2 _velocity = Vector2.zero;
        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
        }

        private void Start()
        {
            DrawVectors = _DrawVectors;
            IsSpeedConstant = _IsSpeedConstant;
            if (_IsSpeedConstant)
            {
                constantSpeed = Random.value * CONSTANT_SPEED_MIN_VALUE + CONSTANT_SPEED_MIN_VALUE;
            }
        }

        public void SetInput(float x_axis, float y_axis)
        {
            _steering = Vector2.ClampMagnitude(new Vector2(x_axis, y_axis), 1.0f);
            _acceleration = _steering * STEERING_ACCEL;
        }

        private void FixedUpdate()
        {
            if (_IsSpeedConstant)
            {
                _velocity = _acceleration.normalized * constantSpeed;
            }
            else
            {
                _velocity += _acceleration * Time.fixedDeltaTime;
            }
            _velocity = Vector2.ClampMagnitude(_velocity, _MAX_SPEED);
            Vector3 position = transform.position;
            position += (Vector3)(_velocity * Time.fixedDeltaTime);
            transform.position = position;
        }

        public void ResetVectors()
        {
            _steering = Vector2.zero;
            _acceleration = Vector2.zero;
            _velocity = Vector2.zero;
        }

        private void Update()
        {
            _steering_line.SetPosition(1, _steering * STEERING_LINE_SCALE);
            _velocity_line.SetPosition(1, _velocity * VELOCITY_LINE_SCALE);
        }
    }
}