using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        public virtual void Attack()
        {

        }

        public virtual void ReceiveDamge()
        {
            gameObject.SetActive(false);
        }
    }
}

