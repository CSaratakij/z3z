using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    [RequireComponent(typeof(DamageAble))]
    public class StandStilEnemy : Enemy
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
            ReceiveDamge();
        }
    }
}

