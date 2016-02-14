using UnityEngine;
using System.Collections;

namespace VRGameJam2016
{
    public class Speedometer : MonoBehaviour {

        public Material speedometerMat;
        public LinearMover movement;

        void Update() {
            speedometerMat.SetFloat("_Tint", movement.Speed);
        }                
    }
}