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
                    var currentSpeed = linearMover.Speed;

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

            transform.Rotate(Vector3.up,
                turningAxis * Time.deltaTime * Constants.PlayerCycleTurnFactor);
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
