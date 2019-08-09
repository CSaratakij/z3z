using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField]
        bool useByPlayer = true;

        [SerializeField]
        Vector3 velocity;

        [SerializeField]
        string targetTag;

        void Reset()
        {
            useByPlayer = true;
            velocity = (Vector3.up * 40.0f);
            targetTag = "Player";
        }

        void OnTriggerEnter(Collider collider)
        {
            JumpHandler(collider.gameObject);
        }

        void JumpHandler(GameObject obj)
        {
            if (useByPlayer) {
                LaunchPlayer(obj);
            }
            else {
                LaunchTarget(obj);
            }
        }

        void LaunchPlayer(GameObject obj)
        {
            PlayerController player = obj.GetComponent<PlayerController>();

            if (player == null)
                return;

            player.InstantJump(velocity);
        }

        void LaunchTarget(GameObject obj)
        {
            if (!obj.CompareTag(targetTag))
                return;

            Rigidbody rigid = obj.GetComponent<Rigidbody>();

            if (rigid == null)
                return;

            rigid.AddForce(velocity, ForceMode.VelocityChange);
        }
    }
}

