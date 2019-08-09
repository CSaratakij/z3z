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
        string hitTag;

        void Awake()
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
            if (collider.CompareTag(hitTag)) {
                OnHit?.Invoke();
            }
        }
    }
}

