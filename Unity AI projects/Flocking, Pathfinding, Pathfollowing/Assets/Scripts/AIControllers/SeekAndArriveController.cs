using UnityEngine;
using System.Collections;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class SeekAndArriveController : MonoBehaviour
    {
        private const float CHANGE_TIME = 10.0f;
        private bool isActorChanged;
        private IActor _actor;
        public Vector2 _steering = Vector2.zero;
        private SimplePathFollowingActor SAScript;
        private Pathfollowing PFScript;
        private Pathfinding PathFindingScript;
        
        private void Awake()
        {
            _actor = GetComponent<IActor>();
        }
        private void Start()
        {
            isActorChanged = false;
            SAScript = GetComponent<SimplePathFollowingActor>();
            PathFindingScript = GetComponent<Pathfinding>();
        }
        private void Update()
        {
            if (!isActorChanged)
            {
                isActorChanged = true;
            }
        }
        private void FixedUpdate()
        {
            if (SAScript.isPathCalculated)
            {
                _steering = (ToVector2(SAScript.pathpoints[SAScript.currentPoint] - transform.position)).normalized;
            }
           /*if ((Mathf.Abs(ToVector2(SAScript.pathpoints[SAScript.currentPoint]).sqrMagnitude -ToVector2(transform.position).sqrMagnitude)
                ) < 0.5f)*/
                if(Vector2.Distance((SAScript.pathpoints[SAScript.currentPoint]),transform.position)<0.05f)
            {
                if (SAScript.currentPoint <= SAScript.totalPathPoints - 2)
                {
                    SAScript.currentPoint++;
                }
            }
             //if ((Mathf.Abs (ToVector2(SAScript.endPoint).sqrMagnitude -ToVector2(transform.position).sqrMagnitude) ) < 0.1f)
            if ( Vector2.Distance(SAScript.endPoint, transform.position) < 0.05f)
            {
                if (!PFScript)
                {
                    PFScript = GameObject.Find("Pathfollowing").GetComponent<Pathfollowing>();
                }
                SAScript.isPathCalculated = false;
                PathFindingScript.newStartNodeAssigned = false;
                PFScript.RecalculatePath(SAScript.rollNumber);
            }
                // Pass all parameters to the character control script.
                _actor.SetInput(_steering.x, _steering.y);
        }
        private Vector2 ToVector2(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }
    }
}

