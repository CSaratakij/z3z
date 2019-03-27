using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        Vector3 aimDirection;


        void LateUpdate()
        {
            RotatationHandler();
        }

        void RotatationHandler()
        {
            aimDirection = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
        }
    }
}

