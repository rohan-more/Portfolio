using UnityEngine;
using System.Collections;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class PursuitController : MonoBehaviour
    {
        private IActor _actor;
        [SerializeField] PredictionController predictionController;
        private float x_axis, y_axis;
        private void Awake()
        {
            _actor = GetComponent<IActor>();
        }

        private void FixedUpdate()
        {
             x_axis = predictionController.transform.position.x - transform.position.x; 
             y_axis = predictionController.transform.position.y - transform.position.y; 

            // Pass all parameters to the character control script.
            _actor.SetInput(x_axis, y_axis);         
        }
    }
}