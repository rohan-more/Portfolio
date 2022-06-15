using UnityEngine;
using System.Collections;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class AvoidController : MonoBehaviour
    {
        #region CONSTANTS
        private const float TIME_SCALE = 0.15f;
        private const float LATERAL_WEIGHT = 30.0f; // steering force opposite to center
        private const float BREAKING_WEIGHT = 3.0f; // braking force applied to decelerate 
        private const string OBSTACLE_TAG = "CircleObstacle";
        #endregion

        #region SERIALIZED OBJECTS
        [SerializeField] private SimpleActor _simpleActor;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Transform raycastLeftOrigin;
        [SerializeField] private Transform raycastRightOrigin;
        [SerializeField] private bool _linesDrawing = true;
        [SerializeField] private LineRenderer _left;
        [SerializeField] private LineRenderer _right;
        #endregion

        private IActor _actor;
        private float _radius;
        private float x_axis;
        private float y_axis;
        private float randomValue;
        private float playerVelocityMagnitude;
        private Vector2 playerVelocityNormalized;
        private RaycastHit2D hitLeft;
        private RaycastHit2D hitRight;
        private Vector3 offset;
        private Collider2D circleCollider;
        private CircleObstacle[] circles;
        public bool linesDrawing
        {
            get
            {
                return _linesDrawing;
            }
            set
            {
                _linesDrawing = value;
                _left.gameObject.SetActive(_linesDrawing);
                _right.gameObject.SetActive(_linesDrawing);
            }
        }
        private void Awake()
        {
            _actor = GetComponent<IActor>();
            _radius = _renderer.bounds.extents.x;
            circles = FindObjectsOfType<CircleObstacle>();
        }

        private void Start()
        {
            randomValue = Random.value;
            x_axis = (Random.Range(int.MinValue, int.MaxValue) % 2 == 0) ? randomValue : -randomValue;
            y_axis = (Random.Range(int.MinValue, int.MaxValue) % 2 == 0) ? randomValue : -randomValue;
        }

        private void FixedUpdate()
        {
            playerVelocityMagnitude = _simpleActor.Velocity.magnitude;
            playerVelocityNormalized = _simpleActor.Velocity.normalized;
            if (playerVelocityMagnitude > 0.0f)
            {
                raycastLeftOrigin.localPosition = Vector3.ClampMagnitude(Quaternion.AngleAxis(90, Vector3.forward) * playerVelocityNormalized, _radius);
                raycastRightOrigin.localPosition = Vector3.ClampMagnitude(Quaternion.AngleAxis(-90, Vector3.forward) * playerVelocityNormalized, _radius);

                hitLeft = Physics2D.Raycast(raycastLeftOrigin.transform.position, playerVelocityNormalized, playerVelocityMagnitude * TIME_SCALE);
                hitRight = Physics2D.Raycast(raycastRightOrigin.transform.position, playerVelocityNormalized, playerVelocityMagnitude * TIME_SCALE);

                if (hitLeft.collider != null && hitLeft.collider.CompareTag(OBSTACLE_TAG))
                {
                    SteeringForce(hitLeft, raycastLeftOrigin, true);
                }
                else if (hitRight.collider != null && hitRight.collider.CompareTag(OBSTACLE_TAG))
                {
                    SteeringForce(hitRight, raycastRightOrigin, false);
                }
                else if (circleCollider != null)
                {
                    foreach (CircleObstacle circle in circles)
                    {
                        if (circle.gameObject == circleCollider.gameObject)
                        {
                            circle.OriginalCircle();
                            break;
                        }
                    }
                }
            }
            _actor.SetInput(x_axis, y_axis);
        }

        private void Update()
        {
            playerVelocityMagnitude = _simpleActor.Velocity.magnitude;
            playerVelocityNormalized = _simpleActor.Velocity.normalized;
             offset = playerVelocityMagnitude * TIME_SCALE * new Vector3(playerVelocityNormalized.x, playerVelocityNormalized.y);
            _left.SetPosition(0, raycastLeftOrigin.position);
            _left.SetPosition(1, raycastLeftOrigin.position + offset);
            _right.SetPosition(0, raycastRightOrigin.position);
            _right.SetPosition(1, raycastRightOrigin.position + offset);
        }

        /// <summary>
        /// Gives a 2D vector from a 3D vector
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Vector2 ToVector2(Vector3 c)
        {
            return new Vector2(c.x, c.y);
        }

        /// <summary>
        /// Calculates the urgency value for two vectors based on the actor speed
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="isLateral"></param>
        /// <returns></returns>
        private float CalculateUrgency(Vector2 a, Vector2 b, bool isLateral)
        {
            float urgencyValue = (a - b).magnitude / _simpleActor.MaxSpeed;
            if (isLateral)
            {
                return 1.0f - urgencyValue;
            }
            else
            {
                return 1.0f - Mathf.Pow(urgencyValue, 2);
            }

        }

        /// <summary>
        /// Adds a steering force to actor, direction based on which raycast is hit
        /// </summary>
        /// <param name="hit"></param>
        /// <param name="rayOrigin"></param>
        /// <param name="isRight"></param>
        private void SteeringForce(RaycastHit2D hit, Transform rayOrigin, bool isRight)
        {
            Vector2 steeringForce = Vector2.zero;
            circleCollider = hit.collider;
            foreach (CircleObstacle circle in circles)
            {
                if(circle.gameObject == circleCollider.gameObject)
                {
                    circle.SelectedCircle();
                    break;
                }
            }

            Vector2 steerLateral;
            if (isRight)
            {
                steerLateral = (raycastRightOrigin.position - transform.position).normalized;
            }
            else
            {
                steerLateral = (raycastLeftOrigin.position - transform.position).normalized;
            }

            Vector2 breakingDir = -playerVelocityNormalized;
            steeringForce += CalculateUrgency(hit.point, ToVector2(rayOrigin.position), true) * LATERAL_WEIGHT * steerLateral;
            steeringForce += CalculateUrgency(hit.point, ToVector2(rayOrigin.position), false) * BREAKING_WEIGHT * breakingDir;
            x_axis += steeringForce.x;
            y_axis += steeringForce.y;
        }

 
    }
}