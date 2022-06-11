using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace AISandbox
{
    public class Flocking : MonoBehaviour
    {
        private const float SPAWN_RANGE = 10.0f;
        public float neighbourDistance = 10.0f;
        public float seperationWeight = 1.0f;
        public float alignmentWeight = 1.0f;
        public float cohesionWeight = 0.5f;
        public FlockingController _flockingActorPrefab;
        [SerializeField]
        private float noOfActors;

        public List<FlockingController> flockActorList = new List<FlockingController>();

        private FlockingController CreateFlockingActor()
        {
            FlockingController newActor = Instantiate(_flockingActorPrefab, this.transform);
            newActor.transform.position = new Vector3(Random.Range(-SPAWN_RANGE, SPAWN_RANGE), Random.Range(-SPAWN_RANGE, SPAWN_RANGE), 0.0f);
            newActor.flockingManager = this;
            return newActor;
        }

        private void Start()
        {

            for (int i = 0; i < noOfActors; ++i)
            {
                flockActorList.Add(CreateFlockingActor());
            }

            flockActorList[10].debugFlag = true;
        }


        public void FixedUpdate()
        {
            for (int i = 0; i < noOfActors; i++)
            {
                flockActorList[i].MoveActor();
            }
        }



    }
}