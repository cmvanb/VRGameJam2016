﻿using UnityEngine;
using System.Collections;

namespace VRGameJam2016
{
    public class PlayerController : MonoBehaviour
    {
        public float SpeedPercentage
        {
            get
            {
                return Mathf.Clamp01((linearMover.Speed - Constants.CycleSpeedMin)
                    / (Constants.CycleSpeedMax - Constants.CycleSpeedMin));
            }
        }

        public GameObject Cycle
        {
            get
            {
                return cycle;
            }
        }

        public bool Driving
        {
            get { return driving; }
            set { driving = value; }
        }
        
        [SerializeField]
        private GameObject cycle;

        [SerializeField]
        private HandListener handListener;
        
        private bool driving = true;

        private bool inputReceivedLastFrame = false;

        private LinearMover linearMover = null;

        private float turnValue = 0f;

        private float turnAcceleration = 10f;

        void Start()
        {
            linearMover = GetComponent<LinearMover>();
            
            gameObject.SendMessage("AccelerateTo", new object[]{
                Constants.CycleSpeedMin,
                Constants.CycleAcceleration
            });

            if (cycle == null)
            {
                cycle = transform.Find("CycleBody").gameObject;
            }
        }
        
        void Update()
        {
            if (driving)
            {
                UpdateDriving();
            }
            else
            {
                UpdateWalking();
            }
        }

        public void UpdateDriving()
        {
            var currentSpeed = linearMover.Speed;

            // Map input to accel/decel.
            if (Input.GetButton("Accelerate")
                || (handListener && handListener.ShouldAccelerate))
            {
                gameObject.SendMessage("AccelerateTo", new object[]{
                    Constants.CycleSpeedMax,
                    Constants.CycleAcceleration
                });

                inputReceivedLastFrame = true;
            }
            else if (Input.GetButton("Decelerate")
                || (handListener && handListener.ShouldDecelerate))
            {
                gameObject.SendMessage("AccelerateTo", new object[]{
                    Constants.CycleSpeedMin,
                    Constants.CycleDeceleration
                });
                
                inputReceivedLastFrame = true;
            }
            else
            {
                if (inputReceivedLastFrame == true)
                {
                    var targetSpeed = linearMover.TargetSpeed;
                    
                    // We do this to make sure the cycle doesn't continue accelerating when the
                    // player stops providing input.
                    gameObject.SendMessage("AccelerateTo", new object[]{
                        targetSpeed,
                        Constants.CycleAcceleration
                    });

                    inputReceivedLastFrame = false;
                }
            }

            // Calculate turn math.
            var turningAxis = Input.GetAxis("Horizontal");

            if (handListener != null)
            {
                turningAxis = handListener.TurningAxis;
            }

            var speedPercentage = (currentSpeed - Constants.CycleSpeedMin)
                / (Constants.CycleSpeedMax - Constants.CycleSpeedMin);
            
            var turnFactor = Constants.CycleTurnFactorMin
                + ((Constants.CycleTurnFactorMax - Constants.CycleTurnFactorMin)
                * (1 - speedPercentage));

            var targetTurnValue = turningAxis * turnFactor * Time.deltaTime;

            turnValue = Mathf.Lerp(turnValue, targetTurnValue, turnAcceleration * Time.deltaTime);
            
            transform.Rotate(Vector3.up, turnValue);

            // Adjust cycle lean to match turn.
            var targetLeanAngle = 0f;
            
            if (turningAxis < -0.1f
                || turningAxis > 0.1f)
            {            
                var turnPercentage = turnFactor / Constants.CycleTurnFactorMax;

                targetLeanAngle = turnPercentage * -turningAxis * Constants.CycleLeanAngleMax;
            }
            
            var currentLeanAngle = cycle.transform.eulerAngles.z;
                
            var newLeanAngle = Mathf.LerpAngle(currentLeanAngle, targetLeanAngle, 10 * Time.deltaTime);

            cycle.transform.eulerAngles = new Vector3(
                cycle.transform.eulerAngles.x,
                cycle.transform.eulerAngles.y,
                newLeanAngle);
        }

        public void UpdateWalking()
        {
            // TODO...
        }
    }
}
