﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class ObjectiveChecker : MonoBehaviour
    {
        [SerializeField]
        GameObject[] objects;

        [SerializeField]
        GameObject[] optionalObjects;

        public static int BulletCount { get; private set; }

        public int TotalEnemy => objects.Length;
        public int TotalOptionalEnemy => optionalObjects.Length;

        public float Accuracy {
            get {
                if (BulletCount == 0)
                    return 0.0f;
                else
                    return (float)(TotalEnemy + TotalOptionalEnemyPoint()) / (float)BulletCount;
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
            ClearStat();
        }

        void CheckObjective()
        {
            if (!isChecking)
                return;

            for (int i = 0; i < objects.Length; ++i)
            {
                if (objects[i] == null) {
                    Debug.LogError("Some object is missing in objective...");
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

        void OnTriggerEnter(Collider collider)
        {
            if (!collider.CompareTag("Player"))
                return;

            if (GameController.IsGameStart) {
                isChecking = false;
                GameController.GameOver();
            }
        }

        void SubscribeEvent()
        {
            GameController.OnGameStart += OnGameStart;
            GameController.OnGameReset += OnGameReset;
        }

        void UnsubscribeEvent()
        {
            GameController.OnGameStart -= OnGameStart;
            GameController.OnGameReset -= OnGameReset;
        }

        void OnGameStart()
        {
            isChecking = true;
        }

        void OnGameReset()
        {
            ClearStat();
        }

        int TotalOptionalEnemyPoint()
        {
            int result = 0;

            for (int i = 0; i < optionalObjects.Length; ++i)
            {
                if (optionalObjects[i].activeSelf)
                    continue;

                result += 1;
            }

            return result;
        }

        public static void AddBulletCount()
        {
            BulletCount += 1;
        }

        public static void ClearStat()
        {
            BulletCount = 0;
        }

        public bool IsPassObjective()
        {
            for (int i = 0; i < objects.Length; ++i)
            {
                if (objects[i] == null) {
                    Debug.LogError("Some object is missing in objective...");
                    continue;
                }

                if (objects[i].activeSelf) {
                    return false;
                }
            }

            return true;
        }
    }
}

