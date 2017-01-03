using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AISandbox
{
    [RequireComponent(typeof(IFlockingActor))]
    public class FlockingController : MonoBehaviour
    {
        public float neighborDistance = 100.0f;
        public float separationWeight = 1.0f;
        public float alignmentWeight = 10.0f;
        public float cohesionWeight = 1.0f;
        public bool isToggleOn = true;
        private IFlockingActor _actor;
        public Vector2 steering;
        public Vector2 alignmentVector;
        private Vector2 cohesionVector;
        private Vector2 seperationVector;
        private Flocking flockingObject;
        //private FlockingController fActor;
        public float neighbourCount = 0;
        public Vector2 computeAlignmentVector = Vector2.zero;
        public Vector2 computeCohesionVector = Vector2.zero;
        public Vector2 computeSeperationVector = Vector2.zero;
        public Vector2 o_Velocity;
        public OrientedActor oActor;
        public float maxSpeed =0;
        public bool debugFlag = false;
        private float elapsedTime;
        private float currentTime;

        private void Start()
        {
            _actor = GetComponent<IFlockingActor>();
            steering = Vector2.zero;
            oActor = GetComponent<OrientedActor>();
            flockingObject = GameObject.Find("Pathfollowing").GetComponent<Flocking>();
            //fActor = this.GetComponent<FlockingController>();
            elapsedTime = currentTime = Time.fixedTime;
        }

        private void FixedUpdate()
        {
            GetVelocity();
           // GetDrawVectors();
            neighbourCount = 0;
            GetMaxSpeed();
            currentTime = Time.fixedTime;
           if(currentTime - elapsedTime > 5.0f)
            {
                elapsedTime = currentTime;
  separationWeight = Random.Range(0,501);
 alignmentWeight = Random.Range(0, 101);
                cohesionWeight = Random.Range(0, 51);  
            }


           // SetDrawVectors();
           
            alignmentVector = computeAlignment(this);
            neighbourCount = 0;
            cohesionVector = computeCohesion(this);
            neighbourCount = 0;
            seperationVector = computeSeperation(this);
              steering.x = alignmentVector.x * alignmentWeight + cohesionVector.x * cohesionWeight + seperationVector.x * separationWeight;
            steering.y = alignmentVector.y * alignmentWeight + cohesionVector.y * cohesionWeight + seperationVector.y * separationWeight;
            //steering.x = 20;
          //  steering.y = 20;
            //steering.x = seperationVector.x * separationWeight;
            //steering.y = seperationVector.y * separationWeight;



        }

        public void GetDrawVectors()
        {
            isToggleOn = oActor.DrawVectors;


        }

        public void SetDrawVectors(bool toggle)
        {
            oActor.DrawVectors = toggle;


        }
        public void GetVelocity()
        {
            o_Velocity = new Vector2(oActor.Velocity.x, oActor.Velocity.y);


        }

        public void GetMaxSpeed()
        {
            maxSpeed = oActor.MaxSpeed;
            
        }
        public Vector2 computeAlignment(FlockingController fActor)
        {


            foreach (GameObject actor in flockingObject.actors)
            {
                if (fActor != actor)
                {
                    //Debug.Log(fActor);
                    //Debug.Log(actor);
                    if (Vector3.Distance(actor.transform.position, fActor.transform.position) < neighborDistance)
                    {
                        computeAlignmentVector.x += actor.GetComponent<FlockingController>().o_Velocity.x;
                        computeAlignmentVector.y += actor.GetComponent<FlockingController>().o_Velocity.y;
                        neighbourCount++;

                    }
                }
            }


                    if (neighbourCount == 0)
                    {
                        return Vector2.zero;
                    }
                    else
                    {
                computeAlignmentVector.x /= neighbourCount;
                computeAlignmentVector.y /= neighbourCount;

                computeAlignmentVector.Normalize();
                computeAlignmentVector.x *= maxSpeed;
                computeAlignmentVector.y *= maxSpeed;
                computeAlignmentVector = computeAlignmentVector - fActor.o_Velocity;
                return computeAlignmentVector;

            }
                

        }

        public Vector2 computeCohesion(FlockingController fActor)
        {
            computeCohesionVector = Vector2.zero;

            foreach (GameObject actor in flockingObject.actors)
            {
                if (actor != fActor)
                {

                    if (Vector2.Distance(actor.transform.position, fActor.transform.position) < neighborDistance)
                    {
                        computeCohesionVector.x += actor.transform.position.x;
                        computeCohesionVector.y += actor.transform.position.y;
                        neighbourCount++;

                    }
                }
            }
            if (neighbourCount == 0)
            {
                return Vector2.zero;
            }
            else
            {
                computeCohesionVector.x /= neighbourCount;
                computeCohesionVector.y /= neighbourCount;
                computeCohesionVector = new Vector2(computeCohesionVector.x - fActor.transform.position.x, computeCohesionVector.y - fActor.transform.position.y);

                return computeCohesionVector;
            }

        }

        public Vector2 computeSeperation(FlockingController fActor)
        {
            Vector2 steer = Vector2.zero;
            computeSeperationVector = Vector2.zero;

 //           int total_actors = 0;
            foreach (GameObject actor in flockingObject.actors)
            {
   //             ++total_actors;
                if (actor != fActor)
                {
                    float distance = Vector2.Distance(actor.transform.position, fActor.transform.position);

                    if (distance < neighborDistance)
                    {
                        
                        steer.x = fActor.transform.position.x - actor.transform.position.x;
                        steer.y = fActor.transform.position.y - actor.transform.position.y;

                        steer.Normalize();
                        steer *= 1.0f - (distance / neighborDistance);
                        computeSeperationVector += steer;
                        /*computePosition.x /= Vector2.Distance(actor.transform.position, fActor.transform.position);
                        computePosition.y /= Vector2.Distance(actor.transform.position, fActor.transform.position);
                        steer.x += computePosition.x;
                        steer.y += computePosition.y;*/
                        neighbourCount++;
                    }
                }
            }
            
                return computeSeperationVector;
            
            //return steer;
            

        }



        private void Update()
        {

            // Pass all parameters to the character control script.
            _actor.SetFlockingInput(steering.x, steering.y);
           
        }
    }
}