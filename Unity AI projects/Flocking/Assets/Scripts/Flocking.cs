using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace AISandbox {
    public class Flocking : MonoBehaviour {
        private const float SPAWN_RANGE           = 10.0f;

        public FlockingController _flockingActorPrefab;
        public Slider _neighborDistanceSlider;
        public Slider _separationSlider;
        public Slider _alignmentSlider;
        public Slider _cohesionSlider;
        public Toggle toggleDraw;
        private OrientedActor oActor;
        
       
        

        public List<FlockingController> _flock = new List<FlockingController>();

        private FlockingController CreateFlockingActor() {
            FlockingController newActor = Instantiate<FlockingController>(_flockingActorPrefab);
            newActor.gameObject.name = "Flocking Actor";
            newActor.transform.position = new Vector3(Random.Range(-SPAWN_RANGE, SPAWN_RANGE), Random.Range(-SPAWN_RANGE, SPAWN_RANGE), 0.0f );
            newActor.GetComponent<OrientedActor>().initialVelocity = Random.onUnitSphere * Random.Range( 0.0f, newActor.GetComponent<IActor>().MaxSpeed );
            newActor.gameObject.SetActive(true);
            return newActor;
		}

       


        private void Start() {
            _neighborDistanceSlider.value = _flockingActorPrefab.neighborDistance;
            _separationSlider.value = _flockingActorPrefab.separationWeight;
            _alignmentSlider.value = _flockingActorPrefab.alignmentWeight;
            _cohesionSlider.value = _flockingActorPrefab.cohesionWeight;
            //toggleDraw.isOn = _flockingActorPrefab.isToggleOn;

            _neighborDistanceSlider.onValueChanged.AddListener( OnNeighborDistanceSliderValueChanged );
            _separationSlider.onValueChanged.AddListener( OnSeparationSliderValueChanged );
            _alignmentSlider.onValueChanged.AddListener( OnAlignmentSliderValueChanged );
            _cohesionSlider.onValueChanged.AddListener( OnCohesionSliderValueChanged );
            toggleDraw.onValueChanged.AddListener(OnToggleValueChanged);
        

            for( int i=0; i<50; ++i ) {
                _flock.Add( CreateFlockingActor() );
            }

            _flock[40].debugFlag = true;
            oActor = GameObject.Find("Flocking Actor").GetComponent<OrientedActor>();
        }

        private void OnNeighborDistanceSliderValueChanged(float neighborDistance) {
            foreach( FlockingController actor in _flock ) {
                actor.neighborDistance = neighborDistance;
            }
        }

        private void OnSeparationSliderValueChanged(float separationWeight) {
            foreach( FlockingController actor in _flock ) {
                actor.separationWeight = separationWeight;
            }
        }

        private void OnAlignmentSliderValueChanged(float alignmentWeight) {
            foreach( FlockingController actor in _flock ) {
                actor.alignmentWeight = alignmentWeight;
            }
        }

        private void OnCohesionSliderValueChanged(float cohesionWeight) {
            foreach( FlockingController actor in _flock ) {
                actor.cohesionWeight = cohesionWeight;
            }
        }

        private void OnToggleValueChanged(bool toggle)
        {
            foreach (FlockingController actor in _flock)
            {
                actor.SetDrawVectors(toggle);
            }
        }


        /* public Vector2 computeCohesion(FlockingController fActor)
          {
              Vector2 computePosition = Vector2.zero;

              foreach (FlockingController actor in _flock)
              {

                  if (Vector2.Distance(actor.steering, fActor.steering) < fActor.alignmentWeight)
                  {
                      computePosition.x += actor.transform.position.x;
                      computePosition.y += actor.transform.position.y;

                  }
              }
              if (neighbourCount == 0)
              {
                  return Vector2.zero;
              }
              else
              {
                  computePosition.x /= neighbourCount;
                  computePosition.y /= neighbourCount;
                  computePosition = new Vector2(computePosition.x - )
                  return computeVelocity.normalized;
              }

          }

         public Vector2 computeSeperation(FlockingController fActor)
          {
              Vector2 computeVelocity = Vector2.zero;

              foreach (FlockingController actor in _flock)
              {

                  if (Vector2.Distance(actor.steering, fActor.steering) < fActor.alignmentWeight)
                  {
                      fActor.steering.x += actor.steering.x;
                      fActor.steering.y += actor.steering.y;
                      neighbourCount++;

                  }
              }
              if (neighbourCount == 0)
              {
                  return Vector2.zero;
              }
              else
              {
                  fActor.steering.x /= neighbourCount;
                  fActor.steering.x /= neighbourCount;
                  return fActor.steering.normalized;
              }

          }
          */




    }
}