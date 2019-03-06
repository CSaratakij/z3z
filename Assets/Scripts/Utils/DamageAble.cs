using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class DamageAble : MonoBehaviour
    {
        public delegate void _Func();
        public event _Func OnHit;


        void OnDestroy()
        {
            OnHit = null;
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Bullet")) {
                OnHit?.Invoke();
            }
        }
    }
}
