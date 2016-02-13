using UnityEngine;
using System.Collections;

namespace VRGameJam2016
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameObject cycle;
        
        private bool driving = true;

        private bool inputReceivedLastFrame = false;

        private LinearMover linearMover = null;

        private float turnValue = 0f;

        private float turnAcceleration = 10f;

        void Start()
        {
            linearMover = GetComponent<LinearMover>();
            
            gameObject.SendMessage("AccelerateTo", new object[]{
                Constants.PlayerCycleSpeedMin,
                Constants.PlayerCycleAcceleration
            });
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
            
            if (Input.GetButton("Accelerate"))
            {
                gameObject.SendMessage("AccelerateTo", new object[]{
                    Constants.PlayerCycleSpeedMax,
                    Constants.PlayerCycleAcceleration
                });

                inputReceivedLastFrame = true;
            }
            else if (Input.GetButton("Decelerate"))
            {
                gameObject.SendMessage("AccelerateTo", new object[]{
                    Constants.PlayerCycleSpeedMin,
                    Constants.PlayerCycleDeceleration
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
                        Constants.PlayerCycleAcceleration
                    });

                    inputReceivedLastFrame = false;
                }
            }

            // Calculate turn math.
            var turningAxis = Input.GetAxis("Horizontal");

            var speedPercentage = (currentSpeed - Constants.PlayerCycleSpeedMin)
                / (Constants.PlayerCycleSpeedMax - Constants.PlayerCycleSpeedMin);
            
            var turnFactor = Constants.PlayerCycleTurnFactorMin
                + ((Constants.PlayerCycleTurnFactorMax - Constants.PlayerCycleTurnFactorMin)
                * (1 - speedPercentage));

            var targetTurnValue = turningAxis * turnFactor * Time.deltaTime;

            turnValue = Mathf.Lerp(turnValue, targetTurnValue, turnAcceleration * Time.deltaTime);
            
            transform.Rotate(Vector3.up, turnValue);

            // Adjust cycle lean to match turn.
            
            
            //cycle.transform.Rotate(Vector3.forward, turningAxis * turnFactor
        }

        public void UpdateWalking()
        {
            // TODO...
        }

        public void SetDriving(bool driving)
        {
            this.driving = driving;
        }
    }
}
