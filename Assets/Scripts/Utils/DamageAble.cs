using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class DamageAble : MonoBehaviour
    {
        public event Action OnHit;

        [SerializeField]
        bool hitByTrigger = true;

        [SerializeField]
        string hitTag;

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            if (hitTag == string.Empty)
                hitTag = "Bullet";
        }

        void OnDestroy()
        {
            OnHit = null;
        }

        void OnTriggerEnter(Collider collider)
        {
            if (!hitByTrigger)
                return;

            CheckHit(collider.gameObject);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (hitByTrigger)
                return;

            CheckHit(collision.gameObject);
        }

        void CheckHit(GameObject obj)
        {
            if (obj.CompareTag(hitTag)) {
                OnHit?.Invoke();
            }
        }
    }
}

