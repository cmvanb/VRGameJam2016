using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VRGameJam2016
{
    public class Exploder : MonoBehaviour
    {

        public GameObject explosionPrefab;
        public List<ParticleSystem> emittersList = new List<ParticleSystem>();

        public bool makeExplosion;

        public void AddExplosion(Vector3 newPos)
        {
            GameObject newExplo = (GameObject)Instantiate(explosionPrefab);
            newExplo.transform.position = newPos;
            emittersList.Add( newExplo.transform.GetComponentInChildren<ParticleSystem>() );
        }

        void Update()
        {
            if (makeExplosion)
            {
                AddExplosion(transform.position);
                makeExplosion = false;
            }

            /*for (int i = 0; i < emittersList.Count; i++)
            {
                if (emittersList[i].particleCount == 0)
                {
                    Destroy(emittersList[i].transform.root.gameObject);
                    emittersList.Remove(emittersList[i]);
                }
            }*/
        }
    }
}
