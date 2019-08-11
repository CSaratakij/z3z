using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Z3Z
{
    public class Spike : MonoBehaviour
    {
        [SerializeField]
        bool hitByTrigger = true;

        void OnTriggerEnter(Collider other)
        {
            if (!hitByTrigger)
                return;

            HitHandler(other.gameObject);
        }

        void OnCollisionEnter(Collision other)
        {
            if (hitByTrigger)
                return;

            HitHandler(other.gameObject);
        }

        void HitHandler(GameObject other)
        {
            if (other.CompareTag("Player")) {
                GameController.Reset();
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex);
            }
        }
    }
}

