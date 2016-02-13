using UnityEngine;
using System.Collections;

namespace VRGameJam2016
{
    public class CopyPosition : MonoBehaviour
    {
        [SerializeField]
        private Transform transformToCopy;

        void Start()
        {
            if (transformToCopy == null)
            {
                transformToCopy = GameObject.Find("Player").transform;
            }
        }
        
        void Update()
        {
            transform.position = transformToCopy.position;
        }
    }
}
