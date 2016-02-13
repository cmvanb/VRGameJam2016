using UnityEngine;
using System.Collections;

namespace Crossing
{
    public class LinearMover : MonoBehaviour
    {
        [SerializeField]
        private float speedUnitsPerSecond = 0f;
        
        [SerializeField]
        private float maxSpeedUnitsPerSecond = 5f;
        
        [SerializeField]
        private bool loggingEnabled = false;

        private float targetSpeed = 0f;

        private float acceleration = 0f;
        
        void Update()
        {            
            if (this.speedUnitsPerSecond < targetSpeed)
            {
                this.speedUnitsPerSecond += acceleration;
            }
            else if (this.speedUnitsPerSecond > targetSpeed)
            {
                this.speedUnitsPerSecond -= acceleration;
            }
            
            Mathf.Clamp(speedUnitsPerSecond, -maxSpeedUnitsPerSecond, maxSpeedUnitsPerSecond);
            
            var unitsDistanceToMove = Time.deltaTime * speedUnitsPerSecond;
            
            transform.Translate(Vector3.forward * unitsDistanceToMove);

            if (loggingEnabled)
            {
                Debug.Log(this.speedUnitsPerSecond);
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
