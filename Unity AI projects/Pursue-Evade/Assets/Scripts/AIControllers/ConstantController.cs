using UnityEngine;
using System.Collections;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class ConstantController : MonoBehaviour
    {
        private IActor _actor;
        private float x_axis;
        private float y_axis;
        private float randomValue;

        private void Awake()
        {
            _actor = GetComponent<IActor>();
        }
        private void Start()
        {
            randomValue = Random.value;
            x_axis = (Random.Range(int.MinValue, int.MaxValue) % 2 == 0) ? randomValue : -randomValue;
            y_axis = (Random.Range(int.MinValue, int.MaxValue) % 2 == 0) ? randomValue : -randomValue;
        }
        private void FixedUpdate()
        {
            _actor.SetInput(x_axis, y_axis);          
        }
    }
}