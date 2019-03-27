using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Z3Z
{
    public class Restarter : MonoBehaviour
    {
        const int UPDATE_RATE = 30;

        [SerializeField]
        Transform player;


        void Update()
        {
            if (Time.frameCount % UPDATE_RATE == 0) {
                RestartHandler();
            }
        }

        void RestartHandler()
        {
            if (player.position.y < transform.position.y) {
                GameController.Reset();
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex);
            }
        }
    }
}
