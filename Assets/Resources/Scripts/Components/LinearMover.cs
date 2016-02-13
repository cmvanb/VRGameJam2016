using UnityEngine;
using System.Collections;

namespace VRGameJam2016
{
    public class LinearMover : MonoBehaviour
    {
        [SerializeField]
        private float speedUnitsPerSecond = 0f;

        public float Speed
        {
            get { return speedUnitsPerSecond; }
        }
        
        [SerializeField]
        private float maxSpeedUnitsPerSecond = 5f;
        
        [SerializeField]
        private bool loggingEnabled = false;

        private float targetSpeed = 0f;

        public float TargetSpeed
        {
            get { return targetSpeed; }
        }
        
        private float acceleration = 0f;
        
        void Update()
        {
            if (speedUnitsPerSecond < targetSpeed)
            {
                speedUnitsPerSecond += acceleration;

                Mathf.Clamp(speedUnitsPerSecond, speedUnitsPerSecond, targetSpeed);
            }
            else if (speedUnitsPerSecond > targetSpeed)
            {
                speedUnitsPerSecond -= acceleration;

                Mathf.Clamp(speedUnitsPerSecond, targetSpeed, speedUnitsPerSecond);
            }
            
            Mathf.Clamp(speedUnitsPerSecond, -maxSpeedUnitsPerSecond, maxSpeedUnitsPerSecond);

            if (Mathf.Abs(speedUnitsPerSecond) < acceleration)
            {
                speedUnitsPerSecond = 0f;
            }
            
            var unitsDistanceToMove = Time.deltaTime * speedUnitsPerSecond;
            
            transform.Translate(Vector3.forward * unitsDistanceToMove);

            if (loggingEnabled)
            {
                Debug.Log(speedUnitsPerSecond);
            }
        }

        public void ChangeSpeed(object[] args)
        {
            this.speedUnitsPerSecond = (float)args[0];
        }

        public void AccelerateTo(object[] args)
        {
            targetSpeed = (float)args[0];
            
            acceleration = (float)args[1];
        }
    }
}
