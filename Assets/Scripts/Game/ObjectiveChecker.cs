using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class ObjectiveChecker : MonoBehaviour
    {
        [SerializeField]
        GameObject[] objects;


        public static int BulletCount { get; private set; }


        public int TotalEnemy => objects.Length;
        public float Accuracy {
            get {
                if (BulletCount == 0)
                    return 0.0f;
                else
                    return (float)TotalEnemy / (float)BulletCount;

            }
        }

        bool isChecking = false;


        void Awake()
        {
            SubscribeEvent();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void LateUpdate()
        {
            Debug.Log(Accuracy.ToString("F4"));
            CheckObjective();
        }

        void CheckObjective()
        {
            if (!isChecking)
                return;

            for (int i = 0; i < objects.Length; ++i)
            {
                if (objects[i] == null) {
                    Debug.Log("Some object is missing in objective...");
                    continue;
                }

                if (objects[i].activeSelf) {
                    return;
                }
            }

            if (GameController.IsGameStart) {
                isChecking = false;
                GameController.GameOver();
            }
        }

        void SubscribeEvent()
        {
            GameController.OnGameStart += OnGameStart;
        }

        void UnsubscribeEvent()
        {
            GameController.OnGameStart -= OnGameStart;
        }

        void OnGameStart()
        {
            isChecking = true;
        }

        public static void AddBulletCount()
        {
            BulletCount += 1;
        }

        public static void ClearStat()
        {
            BulletCount = 0;
        }
    }
}

