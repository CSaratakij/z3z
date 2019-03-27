using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Z3Z
{
    [RequireComponent(typeof(Timer))]
    public class SceneChanger : MonoBehaviour
    {
        Timer timer;

        void Awake()
        {
            Initialize();
            SubscribeEvent();
        }

        void Start()
        {
            timer.Countdown();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void Initialize()
        {
            timer = GetComponent<Timer>();
        }

        void SubscribeEvent()
        {
            timer.OnStop += OnStop;
        }

        void UnsubscribeEvent()
        {
            timer.OnStop -= OnStop;
        }

        void OnStop()
        {
            SceneManager.LoadScene(0);
        }
    }
}
