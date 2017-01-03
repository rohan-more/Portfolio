using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class FlockingController : MonoBehaviour
    {
        public float neighborDistance = 10.0f;
        public float separationWeight = 1.0f;
        public float alignmentWeight = 1.0f;
        public float cohesionWeight = 0.5f;
        public bool isToggleOn = true;
        private IActor _actor;
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
        

        private void Start()
        {
            _actor = GetComponent<IActor>();
            steering = Vector2.zero;
            oActor = GetComponent<OrientedActor>();
            flockingObject = GameObject.Find("Flocking").GetComponent<Flocking>();
            //fActor = this.GetComponent<FlockingController>();
            
        }

        private void FixedUpdate()
        {
            GetVelocity();
           // GetDrawVectors();
            neighbourCount = 0;
            GetMaxSpeed();
            
           // SetDrawVectors();
           
            alignmentVector = computeAlignment(this);
            neighbourCount = 0;
            cohesionVector = computeCohesion(this);
            neighbourCount = 0;
            seperationVector = computeSeperation(this);
            steering.x = alignmentVector.x * alignmentWeight + cohesionVector.x * cohesionWeight + seperationVector.x * separationWeight;
            steering.y = alignmentVector.y * alignmentWeight + cohesionVector.y * cohesionWeight + seperationVector.y * separationWeight;
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


            foreach (FlockingController actor in flockingObject._flock)
            {
                if (fActor != actor)
                {

                    if (Vector3.Distance(actor.transform.position, fActor.transform.position) < neighborDistance)
                    {
                        computeAlignmentVector.x += actor.o_Velocity.x;
                        computeAlignmentVector.y += actor.o_Velocity.x;
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

            foreach (FlockingController actor in flockingObject._flock)
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
            foreach (FlockingController actor in flockingObject._flock)
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
            _actor.SetInput(steering.x, steering.y);
        }
    }
}