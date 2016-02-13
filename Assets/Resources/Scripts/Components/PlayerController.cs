using UnityEngine;
using System.Collections;

namespace VRGameJam2016
{
    public class PlayerController : MonoBehaviour
    {
        private bool driving = true;

        private bool inputReceivedLastFrame = false;

        private LinearMover linearMover = null;

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
                    // We do this to make sure the cycle doesn't continue accelerating when the
                    // player stops providing input.
                    gameObject.SendMessage("AccelerateTo", new object[]{
                        currentSpeed,
                        Constants.PlayerCycleAcceleration
                    });

                    inputReceivedLastFrame = false;
                }
            }

            var turningAxis = Input.GetAxis("Horizontal");

            var speedPercentage = (currentSpeed - Constants.PlayerCycleSpeedMin)
                / (Constants.PlayerCycleSpeedMax - Constants.PlayerCycleSpeedMin);
            
            var turnFactor = Constants.PlayerCycleTurnFactorMin
                + ((Constants.PlayerCycleTurnFactorMax - Constants.PlayerCycleTurnFactorMin)
                * (1 - speedPercentage));

            var turnValue = turningAxis * turnFactor * Time.deltaTime;

            Debug.Log("turnFactor: " +turnFactor);
            transform.Rotate(Vector3.up, turnValue);
        }

        public void UpdateWalking()
        {
        }

        public void SetDriving(bool driving)
        {
            this.driving = driving;
        }
    }
}
