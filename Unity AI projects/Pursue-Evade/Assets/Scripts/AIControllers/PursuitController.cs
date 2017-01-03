using UnityEngine;
using System.Collections;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class PursuitController : MonoBehaviour
    {
        private IActor _actor;
        private PredictionController predictionControllerScript;

        private void Awake()
        {
            _actor = GetComponent<IActor>();
        }
        private void Start()
        {
            predictionControllerScript = GameObject.Find("Prediction").GetComponent<PredictionController>();
        }

        private void FixedUpdate()
        {
            float x_axis = predictionControllerScript.transform.position.x - transform.position.x;    // FIXME
            float y_axis = predictionControllerScript.transform.position.y - transform.position.y;    // FIXME

            // Pass all parameters to the character control script.
            _actor.SetInput(x_axis, y_axis);         
        }
    }
}