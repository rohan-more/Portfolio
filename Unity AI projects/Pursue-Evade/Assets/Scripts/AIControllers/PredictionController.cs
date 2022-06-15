using UnityEngine;
using System.Collections;

namespace AISandbox
{
    public class PredictionController : MonoBehaviour
    {
        [SerializeField] private PursuitAndEvasion pursuitAndEvasionScript;
        [SerializeField]
        private SimpleActor _target_actor;
        [SerializeField]
        private Transform _pursuing_actor;
        private Vector3 targetActorVelocity;
        private float futureTime;
        private const float DISTANCE = 2.5f;
        private void Start()
        {
            InvokeRepeating(nameof(ResetLevel), 2.0f, 1.0f);
        }
        private void Update()
        {
            targetActorVelocity = new Vector3(_target_actor.Velocity.x, _target_actor.Velocity.y);
            futureTime = Vector3.Distance(_target_actor.transform.position, _pursuing_actor.transform.position) / _target_actor.MAX_SPEED;
            transform.position = _target_actor.transform.position + targetActorVelocity * futureTime;
        }
        private void ResetLevel()
        {
            if (Mathf.Abs(Vector3.Distance(_pursuing_actor.transform.position, transform.position)) < DISTANCE)
            {
                pursuitAndEvasionScript.ResetActors();
            }
        }
    }
}
