using UnityEngine;
using System.Collections;

namespace VRGameJam2016
{
    public class HandListener : MonoBehaviour
    {
        public bool ShouldAccelerate;
        
        public bool ShouldDecelerate;
        
        public float TurningAxis = 0f;
        
        [SerializeField]
        private PlayerController playerController;
        
        // WARNING: SUPER HACKY GAME JAM CODE. I am not proud of this.
        public void UpdateHands(Leap.HandList hands)
        {
            Leap.Hand hand1 = hands[0];
            Leap.Hand hand2 = hands[1];
        
            TurningAxis = 0f;
                
            if (hand1 != null && hand2 != null)
            {
                // two hands
                
                if (!playerController.Driving)
                {
                    playerController.Driving = true;
                }

                Leap.Hand leftHand = hands[0].IsLeft ? hands[0] : hands[1];
                Leap.Hand rightHand = hands[0].IsRight ? hands[0] : hands[1];

                var maxDifference = 200f;

                var actualDifference = Mathf.Clamp(
                    leftHand.PalmPosition.y - rightHand.PalmPosition.y,
                    -maxDifference,
                    maxDifference);

                TurningAxis = actualDifference / maxDifference;
                
                // TODO: do accel/decel
            }
            else if (hand1 != null)
            {
                // one hand
                
                if (playerController.Driving)
                {
                    playerController.Driving = false;
                }

                // TODO: do walking mechanics...
            }
            else
            {
                // no hands
            }
        }
    }
}
