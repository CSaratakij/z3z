using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    [RequireComponent(typeof(Timer))]
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        Timer timerFireRate;

        [SerializeField]
        int totalBulletPooling;

        [SerializeField]
        GameObject bulletPrefab;

        [SerializeField]
        Transform barrel;


        bool isFireAble = true;
        Bullet[] bullets;


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
            bullets = new Bullet[totalBulletPooling];

            for (int i = 0; i < totalBulletPooling; ++i)
            {
                bullets[i] = Instantiate(bulletPrefab).GetComponent<Bullet>();
                bullets[i].gameObject.SetActive(false);
            }
        }

        void SubscribeEvent()
        {
            timerFireRate.OnStop += timerFireRate_OnStop;
        }

        void UnsubscribeEvent()
        {
            timerFireRate.OnStop -= timerFireRate_OnStop;
        }

        void timerFireRate_OnStop()
        {
            timerFireRate.Reset();
            isFireAble = true;
        }

        void Fire(Vector3 direction)
        {
            foreach (Bullet obj in bullets)
            {
                if (obj.gameObject.activeSelf)
                    continue;

                obj.SetMovDirection(direction);
                obj.gameObject.transform.position = barrel.position;
                obj.gameObject.SetActive(true);

                break;
            }
        }

        public void PullTrigger(Vector3 aimDirection)
        {
            if (!isFireAble)
                return;

            isFireAble = false;
            Fire(aimDirection);

            timerFireRate.Countdown();
        }
    }
}
