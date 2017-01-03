using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace AISandbox {
    public class Flocking : MonoBehaviour {
        private const float SPAWN_RANGE           = 10.0f;
        public  int TOTALACTORS = 8;
        public  int PLAYERACTORS = 1;
        public int Total = 2;
        public GameObject[] actors;
        public GameObject[] player_actor;
        public GameObject actorPrefab;
        public GameObject playerPrefab;
        public Pathfinding[] PFScripts;
        public SimplePathFollowingActor[] SActor;
        public SeekAndArriveController[] _seekArriveScript;
     
        public StartingPositionController[] _startingScript;
        public StartingPositionController _startPosController;

        public FlockingController[] _flockingControllerScript;
        public Flocking_SimpleActor[] _flockingSimpleActorScript;
        public OrientedActor[] _orientedActorScript;
        public PlayerController _playerControllerScript;
        public StartingPositionController[] _playerStartingScript;
        public SimpleActor _simpleActorScript;
        public GridNode[] positions;
        private Grid _grid;
        //public FlockingController _flockingActorPrefab;
        //public Slider _neighborDistanceSlider;
        //public Slider _separationSlider;
        //public Slider _alignmentSlider;
        //public Slider _cohesionSlider;
        //public Toggle toggleDraw;
        public OrientedActor oActor;
        
       
        
      

        /*   private FlockingController CreateFlockingActor() {
               FlockingController newActor = Instantiate<FlockingController>(_flockingActorPrefab);
               newActor.gameObject.name = "Flocking Actor";
               newActor.transform.position = new Vector3(Random.Range(-SPAWN_RANGE, SPAWN_RANGE), Random.Range(-SPAWN_RANGE, SPAWN_RANGE), 0.0f );
               newActor.GetComponent<OrientedActor>().initialVelocity = Random.onUnitSphere * Random.Range( 0.0f, newActor.GetComponent<IActor>().MaxSpeed );
               newActor.gameObject.SetActive(true);
               return newActor;
           }*/

        private void CreateActors()
        {
            actors = new GameObject[TOTALACTORS];
            player_actor = new GameObject[PLAYERACTORS];
            PFScripts = new Pathfinding[TOTALACTORS];
            SActor = new SimplePathFollowingActor[TOTALACTORS];
            _flockingControllerScript = new FlockingController[PLAYERACTORS];
            _flockingSimpleActorScript = new Flocking_SimpleActor [PLAYERACTORS];
            _orientedActorScript = new OrientedActor[PLAYERACTORS];
            _seekArriveScript = new SeekAndArriveController[TOTALACTORS];
            _startingScript = new StartingPositionController[PLAYERACTORS];
           /// _playerControllerScript = GameObject.Find("Player").AddComponent<PlayerController>();
            //_simpleActorScript = GameObject.Find("Player").AddComponent<SimpleActor>();
            _playerControllerScript = new PlayerController();
            _simpleActorScript = new SimpleActor();


            // _flockingActorPrefab = Instantiate<FlockingController>(_flockingActorPrefab);
            for (int i = 0; i < PLAYERACTORS; i++)
            {
                
                    player_actor[i] = Instantiate(playerPrefab, transform) as GameObject;
                player_actor[i].name = "Player";
                    _startingScript[i] = player_actor[i].GetComponent<StartingPositionController>();
                    _playerControllerScript = player_actor[i].GetComponent<PlayerController>();
                    _simpleActorScript = player_actor[i].GetComponent<SimpleActor>();
                player_actor[i].transform.position = new Vector3(Random.Range(-SPAWN_RANGE, SPAWN_RANGE), Random.Range(-SPAWN_RANGE, SPAWN_RANGE), 0.0f);
                player_actor[i].gameObject.SetActive(true);
            }


                for (int i = 0; i < TOTALACTORS; i++)
                { 
                    actors[i] = Instantiate(actorPrefab, transform) as GameObject;
                    actors[i].name = "Actor";
                    PFScripts[i] = actors[i].GetComponent<Pathfinding>();
                    SActor[i] = actors[i].GetComponent<SimplePathFollowingActor>();
                    _seekArriveScript[i] = actors[i].GetComponent<SeekAndArriveController>();
                    actors[i].transform.position = new Vector3(positions[i].transform.position.x, positions[i].transform.position.y, 0.0f);
                    SActor[i].rollNumber = i;
                
                actors[i].gameObject.SetActive(true);
            }

  
        }




        private void Start() {
            /*  _neighborDistanceSlider.value = _flockingActorPrefab.neighborDistance;
              _separationSlider.value = _flockingActorPrefab.separationWeight;
              _alignmentSlider.value = _flockingActorPrefab.alignmentWeight;
              _cohesionSlider.value = _flockingActorPrefab.cohesionWeight;
              //toggleDraw.isOn = _flockingActorPrefab.isToggleOn;

              _neighborDistanceSlider.onValueChanged.AddListener( OnNeighborDistanceSliderValueChanged );
              _separationSlider.onValueChanged.AddListener( OnSeparationSliderValueChanged );
              _alignmentSlider.onValueChanged.AddListener( OnAlignmentSliderValueChanged );
              _cohesionSlider.onValueChanged.AddListener( OnCohesionSliderValueChanged );
              toggleDraw.onValueChanged.AddListener(OnToggleValueChanged);*/

            Total = 8;
            for( int i=0; i<50; ++i ) {
         //       _flock.Add( CreateActors() );
           }

            actorPrefab = Resources.Load<GameObject>("Prefabs/SimpleActor");
            playerPrefab = Resources.Load<GameObject>("Prefabs/PlayerActor");

           // _startPosController = GameObject.Find("Player").GetComponent<StartingPositionController>();
            _grid = GameObject.Find("Grid").GetComponent<Grid>();
            positions = new GridNode[]{
                _grid.GetNode(1, 1),
                 _grid.GetNode(5, 1) ,
                 _grid.GetNode(2,11),
                 _grid.GetNode(2,19),
                 _grid.GetNode(1,33),
                 _grid.GetNode(2,38),
                 _grid.GetNode(16,39),
                 _grid.GetNode(27,37)
                    };
            CreateActors();
            //_flock[40].debugFlag = true;
            oActor = GameObject.Find("Actor").GetComponent<OrientedActor>();

        }

        //private void OnNeighborDistanceSliderValueChanged(float neighborDistance) {
        //    foreach( FlockingController actor in _flock ) {
        //        actor.neighborDistance = neighborDistance;
        //    }
        //}

        //private void OnSeparationSliderValueChanged(float separationWeight) {
        //    foreach( FlockingController actor in _flock ) {
        //        actor.separationWeight = separationWeight;
        //    }
        //}

        //private void OnAlignmentSliderValueChanged(float alignmentWeight) {
        //    foreach( FlockingController actor in _flock ) {
        //        actor.alignmentWeight = alignmentWeight;
        //    }
        //}

        //private void OnCohesionSliderValueChanged(float cohesionWeight) {
        //    foreach( FlockingController actor in _flock ) {
        //        actor.cohesionWeight = cohesionWeight;
        //    }
        //}

        //private void OnToggleValueChanged(bool toggle)
        //{
        //    foreach (FlockingController actor in _flock)
        //    {
        //        actor.SetDrawVectors(toggle);
        //    }
        //}


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