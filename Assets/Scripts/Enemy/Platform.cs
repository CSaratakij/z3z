using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    [RequireComponent(typeof(Timer))]
    public class Platform : MonoBehaviour
    {
        Timer timer;

        void Awake()
        {
            Initialize();
            SubscribeEvent();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) {
                timer.Countdown();
            }
        }

        void Initialize()
        {
            timer = GetComponent<Timer>();
        }

        void SubscribeEvent()
        {
            timer.OnStop += OnTimerStop;
        }

        void UnsubscribeEvent()
        {
            timer.OnStop -= OnTimerStop;
        }

        void OnTimerStop()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}

