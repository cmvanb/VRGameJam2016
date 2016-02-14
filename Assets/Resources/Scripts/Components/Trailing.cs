using UnityEngine;
using System.Collections;

namespace VRGameJam2016 {
    public class Trailing : MonoBehaviour
    {
        public GameObject trailPrefab;

        private LinearMover movement;
        private Vector3 previousPosition = Vector3.zero;
        private Transform myTranny;
        private GameObject tempTrail;
        private float trailScale = 0f;
        private float distanceToPreviousPosition = 0f;
        private Vector3 trailOffset = new Vector3(0,0,-2f);
        private GameObject trailHolder;

        void Start() 
        {
            movement = gameObject.GetComponent<LinearMover>();
            myTranny = transform;
            previousPosition = myTranny.position;
            trailHolder = new GameObject();
            trailHolder.name = "[TrailHolder]";
        }

        void Update()
        {
            if (movement.Speed == 0) return;

            distanceToPreviousPosition = Vector3.Distance(myTranny.position, previousPosition);

            MakeTrail();

            previousPosition = myTranny.position;
        }

        void MakeTrail()
        {
            tempTrail = (GameObject)Instantiate(trailPrefab);
            tempTrail.transform.position = myTranny.position + trailOffset;
            tempTrail.transform.LookAt(previousPosition);
            tempTrail.transform.localScale = new Vector3(1, 1, distanceToPreviousPosition);
            tempTrail.transform.parent = trailHolder.transform;
        }
    }
}
