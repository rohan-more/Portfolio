using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class FlockingController : MonoBehaviour
    {
        public bool isToggleOn = true;
        private Vector2 steering = Vector2.zero;
        private Vector2 alignmentVector;
        private Vector2 cohesionVector;
        private Vector2 seperationVector;

        private float neighbourCount = 0;
        private Vector2 computeAlignmentVector = Vector2.zero;
        private Vector2 computeCohesionVector = Vector2.zero;
        private Vector2 computeSeperationVector = Vector2.zero;
        private Vector2 o_Velocity;
        public OrientedActor oActor;
        public Flocking flockingManager;
        private float maxSpeed = 0;
        public bool debugFlag = false;

        private void FixedUpdate()
        {
            GetVelocity();
            // GetDrawVectors();
            GetMaxSpeed();
            // SetDrawVectors();
            MoveActor();
        }

        public void MoveActor()
        {
            ComputeAlignment();
            ComputeCohesion();
            ComputeSeperation();
            steering = alignmentVector * flockingManager.AlignmentWeight + cohesionVector * flockingManager.CohesionWeight + seperationVector * flockingManager.SeperationWeight;
        }


        /// <summary>
        /// Calculate the alignment vector based on neighbor count
        /// </summary>
        public void ComputeAlignment()
        {
            foreach (FlockingController actor in flockingManager.flockActorList)
            {
                if (this != actor)
                {
                    if (Vector3.Distance(actor.transform.position, transform.position) < flockingManager.NeighbourDistance)
                    {
                        computeAlignmentVector += actor.o_Velocity;
                        neighbourCount++;
                    }
                }
            }


            if (neighbourCount == 0)
            {
                alignmentVector = computeAlignmentVector = Vector2.zero;
            }
            else
            {
                computeAlignmentVector /= neighbourCount;
                computeAlignmentVector.Normalize();
                computeAlignmentVector *= maxSpeed;
                computeAlignmentVector -= o_Velocity;
                neighbourCount = 0;
                alignmentVector = computeAlignmentVector;
            }
        }

        /// <summary>
        /// Calculate cohesion vector based on neighbor count
        /// </summary>
        public void ComputeCohesion()
        {
            computeCohesionVector = Vector2.zero;

            foreach (FlockingController actor in flockingManager.flockActorList)
            {
                if (actor != this)
                {
                    if (Vector2.Distance(actor.transform.position, transform.position) < flockingManager.NeighbourDistance)
                    {
                        computeCohesionVector += (Vector2) actor.transform.position;
                        neighbourCount++;

                    }
                }
            }
            if (neighbourCount == 0)
            {
                cohesionVector = computeCohesionVector = Vector2.zero;
            }
            else
            {
                computeCohesionVector /= neighbourCount;
                computeCohesionVector = new Vector2(computeCohesionVector.x - transform.position.x, computeCohesionVector.y - transform.position.y);
                neighbourCount = 0;
                cohesionVector = computeCohesionVector;
            }

        }

        /// <summary>
        /// Compute the seperation vector
        /// </summary>

        public void ComputeSeperation()
        {
            Vector2 steer = Vector2.zero;
            computeSeperationVector = Vector2.zero;

            foreach (FlockingController actor in flockingManager.flockActorList)
            {
                if (actor != this)
                {
                    float distance = Vector2.Distance(actor.transform.position, transform.position);

                    if (distance < flockingManager.NeighbourDistance)
                    {
                        steer = transform.position - actor.transform.position;
                        steer.Normalize();
                        steer *= 1.0f - (distance / flockingManager.NeighbourDistance);
                        computeSeperationVector += steer;
                        neighbourCount++;
                    }
                }
            }
            seperationVector = computeSeperationVector;
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



        private void Update()
        {
            // Pass all parameters to the character control script.
            oActor.SetInput(steering.x, steering.y);
        }
    }
}