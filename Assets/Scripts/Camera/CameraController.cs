using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        [SerializeField]
        Vector3 offset;

        [SerializeField]
        float damp;


        void Update()
        {
            InputHandler();
        }

        void LateUpdate()
        {
            FollowTargetHandler();
        }

        void InputHandler()
        {

        }

        void FollowTargetHandler()
        {

        }
    }
}
