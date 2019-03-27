using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class EnemyShooter : StandStilEnemy
    {
        [SerializeField]
        Transform target;

        [SerializeField]
        LayerMask targetLayer;

        [SerializeField]
        Gun gun;

        bool isTargetVisible = false;

        Vector3 aimDirection;
        RaycastHit hit;


        void Update()
        {
            ShootHandler();
        }

        void FixedUpdate()
        {
            CheckTargetVisible();
        }

        void LateUpdate()
        {
            RotatationHandler();
        }

        void CheckTargetVisible()
        {
            aimDirection = (target.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, aimDirection, out hit, 100.0f, targetLayer)) {
                isTargetVisible = hit.collider.gameObject.CompareTag("Player");
            }
        }

        void ShootHandler()
        {
            if (!isTargetVisible)
                return;

            gun.PullTrigger(aimDirection);
        }

        void RotatationHandler()
        {
            if (!isTargetVisible)
                return;

            transform.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
        }
    }
}

