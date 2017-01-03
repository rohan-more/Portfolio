using UnityEngine;
using System.Collections;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class AvoidController : MonoBehaviour
    {
        private const float timeScale = 0.35f;
        private const float lateralWeight = 25.0f;
        private const float breakingWeight = 3.0f;

        private IActor _actor;
        private SimpleActor _simpleActor;
        private SpriteRenderer _renderer;
        private float _radius;
        private float x_axis;
        private float y_axis;
        private float randomValue;
        private float playerVelocityMagnitude;
        private Vector2 playerVelocityNormalized;
        private Transform raycastLeftOrigin;
        private Transform raycastRightOrigin;
        private Collider2D circleCollider;
        [SerializeField]
        private bool _linesDrawing = true;
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
        
        public LineRenderer _left;
        
        public LineRenderer _right;

        private void Awake()
        {
            _actor = GetComponent<IActor>();
            _simpleActor = GetComponent<SimpleActor>();
            _renderer = GetComponent<SpriteRenderer>();
            _radius = _renderer.bounds.extents.x;
            raycastLeftOrigin = transform.GetChild(2);
            raycastRightOrigin = transform.GetChild(3);
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
                RaycastHit2D hitLeft;
                RaycastHit2D hitRight;

                hitLeft = Physics2D.Raycast(raycastLeftOrigin.transform.position, playerVelocityNormalized, playerVelocityMagnitude * timeScale);
                hitRight = Physics2D.Raycast(raycastRightOrigin.transform.position, playerVelocityNormalized, playerVelocityMagnitude * timeScale);

                if (hitLeft.collider != null && hitLeft.collider.CompareTag("CircleObstacle"))
                {
                    SteeringForce(hitLeft, raycastLeftOrigin, true);
                }
                else if (hitRight.collider != null && hitRight.collider.CompareTag("CircleObstacle"))
                {
                    SteeringForce(hitRight, raycastRightOrigin, false);
                }
                else if (circleCollider != null)
                {
                    circleCollider.gameObject.GetComponent<CircleObstacle>().OriginalCircle();
                    circleCollider = null;
                }
            }
            _actor.SetInput(x_axis, y_axis);
        }

        private void Update()
        {
            playerVelocityMagnitude = _simpleActor.Velocity.magnitude;
            playerVelocityNormalized = _simpleActor.Velocity.normalized;
            Vector3 offset = new Vector3(playerVelocityNormalized.x, playerVelocityNormalized.y) * playerVelocityMagnitude * timeScale;
            _left.SetPosition(0, raycastLeftOrigin.position);
            _left.SetPosition(1, raycastLeftOrigin.position + offset);
            _right.SetPosition(0, raycastRightOrigin.position);
            _right.SetPosition(1, raycastRightOrigin.position + offset);
        }

        private Vector2 ToVector2(Vector3 c)
        {
            return new Vector2(c.x, c.y);
        }

        private float CalculateUrgency(Vector2 a, Vector2 b, bool isLateral)
        {
            float urgencyValue = (a - b).magnitude / _simpleActor.MaxSpeed;
            if (isLateral)
            {
                return (1.0f - urgencyValue);
            }
            else
            {
                return (1.0f - (urgencyValue * urgencyValue));
            }

        }

        private void SteeringForce(RaycastHit2D hit, Transform rayOrigin, bool isRight)
        {
            Vector2 steeringForce = Vector2.zero;
            circleCollider = hit.collider;
            circleCollider.gameObject.GetComponent<CircleObstacle>().SelectedCircle();
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
            steeringForce += (steerLateral * CalculateUrgency(hit.point, ToVector2(rayOrigin.position), true) * lateralWeight);
            steeringForce += (breakingDir * CalculateUrgency(hit.point, ToVector2(rayOrigin.position), false) * breakingWeight);
            x_axis += steeringForce.x;
            y_axis += steeringForce.y;
        }

 
    }
}