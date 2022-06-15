using UnityEngine;
using System.Collections;

namespace AISandbox
{
    public class SimpleActor : MonoBehaviour, IActor
    {
        #region CONSTANTS
        private const float MAX_SPEED = 20.0f;
        private const float STEERING_ACCEL = 10 * MAX_SPEED;
        private const float VELOCITY_LINE_SCALE = 0.1f;
        private const float STEERING_LINE_SCALE = VELOCITY_LINE_SCALE * MAX_SPEED;
        #endregion
        [SerializeField]  private bool _DrawVectors = true;
        [SerializeField] private LineRenderer _steering_line;
        [SerializeField] private LineRenderer _velocity_line;
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
        public float radius;
        private Vector2 _steering = Vector2.zero;
        private Vector2 _acceleration = Vector2.zero;
        private Vector2 _velocity = Vector2.zero;

        private void Start()
        {
            DrawVectors = _DrawVectors;
            radius = GetComponent<SpriteRenderer>().bounds.extents.x;
        }

        public void SetInput(float x_axis, float y_axis)
        {
            _steering = Vector2.ClampMagnitude(new Vector2(x_axis, y_axis), 1.0f);
            _acceleration = _steering * STEERING_ACCEL;
        }

        public float MaxSpeed
        {
            get { return MAX_SPEED; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
        }

        private void FixedUpdate()
        {
            _velocity += _acceleration * Time.fixedDeltaTime;
            _velocity = Vector2.ClampMagnitude(_velocity, MAX_SPEED);
            Vector3 position = transform.position;
            position += (Vector3)(_velocity * Time.fixedDeltaTime);
            transform.position = position;
        }

        private void Update()
        {
            _steering_line.SetPosition(1, _steering * STEERING_LINE_SCALE);
            _velocity_line.SetPosition(1, _velocity * VELOCITY_LINE_SCALE);
            // The steering is reset every frame so SetInput() must be called every frame for continuous steering.
            _steering = Vector2.zero;
            _acceleration = Vector2.zero;
        }
    }
}