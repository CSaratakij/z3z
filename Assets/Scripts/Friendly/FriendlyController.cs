using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Z3Z
{
    [RequireComponent(typeof(DamageAble))]
    public class FriendlyController : MonoBehaviour
    {
        DamageAble damageAble;


        void Awake()
        {
            Initialize();
            SubscribeEvent();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void Initialize()
        {
            damageAble = GetComponent<DamageAble>();
        }

        void SubscribeEvent()
        {
            damageAble.OnHit += OnHit;
        }

        void UnsubscribeEvent()
        {
            damageAble.OnHit -= OnHit;
        }

        void OnHit()
        {
            GameController.Reset();
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}

