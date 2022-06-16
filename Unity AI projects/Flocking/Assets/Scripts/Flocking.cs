using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace AISandbox
{
    public class Flocking : MonoBehaviour
    {
        private const float SPAWN_RANGE = 10.0f;
        private float neighbourDistance = 10.0f;
        private float seperationWeight = 1.0f;
        private float alignmentWeight = 1.0f;
        private float cohesionWeight = 0.5f;
        public FlockingController _flockingActorPrefab;
        [SerializeField] private float noOfActors;
        [SerializeField] private int debugActorIndex;

        public List<FlockingController> flockActorList = new List<FlockingController>();

        public float NeighbourDistance { get => neighbourDistance; set => neighbourDistance = value; }
        public float SeperationWeight { get => seperationWeight; set => seperationWeight = value; }
        public float AlignmentWeight { get => alignmentWeight; set => alignmentWeight = value; }
        public float CohesionWeight { get => cohesionWeight; set => cohesionWeight = value; }

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

            flockActorList[debugActorIndex].debugFlag = true;
        }
    }
}