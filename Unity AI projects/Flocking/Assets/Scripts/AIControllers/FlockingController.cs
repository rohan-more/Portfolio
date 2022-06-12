﻿using UnityEngine;
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
            computeAlignment();
            computeCohesion();
            computeSeperation();
            steering.x = alignmentVector.x * flockingManager.alignmentWeight + cohesionVector.x * flockingManager.cohesionWeight + seperationVector.x * flockingManager.seperationWeight;
            steering.y = alignmentVector.y * flockingManager.alignmentWeight + cohesionVector.y * flockingManager.cohesionWeight + seperationVector.y * flockingManager.seperationWeight;
        }

        public void computeAlignment()
        {
            foreach (FlockingController actor in flockingManager.flockActorList)
            {
                if (this != actor)
                {
                    if (Vector3.Distance(actor.transform.position, transform.position) < flockingManager.neighbourDistance)
                    {
                        computeAlignmentVector.x += actor.o_Velocity.x;
                        computeAlignmentVector.y += actor.o_Velocity.x;
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
                computeAlignmentVector.x /= neighbourCount;
                computeAlignmentVector.y /= neighbourCount;
                computeAlignmentVector.Normalize();
                computeAlignmentVector.x *= maxSpeed;
                computeAlignmentVector.y *= maxSpeed;
                computeAlignmentVector -= o_Velocity;
                neighbourCount = 0;
                alignmentVector = computeAlignmentVector;
            }
        }

        public void computeCohesion()
        {
            computeCohesionVector = Vector2.zero;

            foreach (FlockingController actor in flockingManager.flockActorList)
            {
                if (actor != this)
                {
                    if (Vector2.Distance(actor.transform.position, transform.position) < flockingManager.neighbourDistance)
                    {
                        computeCohesionVector.x += actor.transform.position.x;
                        computeCohesionVector.y += actor.transform.position.y;
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
                computeCohesionVector.x /= neighbourCount;
                computeCohesionVector.y /= neighbourCount;
                computeCohesionVector = new Vector2(computeCohesionVector.x - transform.position.x, computeCohesionVector.y - transform.position.y);
                neighbourCount = 0;
                cohesionVector = computeCohesionVector;
            }

        }

        public void computeSeperation()
        {
            Vector2 steer = Vector2.zero;
            computeSeperationVector = Vector2.zero;

            foreach (FlockingController actor in flockingManager.flockActorList)
            {
                if (actor != this)
                {
                    float distance = Vector2.Distance(actor.transform.position, transform.position);

                    if (distance < flockingManager.neighbourDistance)
                    {
                        steer.x = transform.position.x - actor.transform.position.x;
                        steer.y = transform.position.y - actor.transform.position.y;

                        steer.Normalize();
                        steer *= 1.0f - (distance / flockingManager.neighbourDistance);
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