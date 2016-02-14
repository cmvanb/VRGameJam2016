using UnityEngine;
using System.Collections;

namespace VRGameJam2016
{
    public class Speedometer : MonoBehaviour {

        public Material speedometerMat;
        private PlayerController movement;
        public float percentage;

        void Start()
        {
            movement = gameObject.GetComponent<PlayerController>();
        }

        void Update() {
            percentage = (1.0f - movement.SpeedPercentage ) ;
            
            speedometerMat.SetFloat("_Cutoff", percentage);
        }                
    }
}