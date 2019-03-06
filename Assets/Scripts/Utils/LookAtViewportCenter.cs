using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class LookAtViewportCenter : MonoBehaviour
    {
        [SerializeField]
        Camera camera;

        [SerializeField]
        Transform barrel;


        Vector3 viewportCenter;
        Vector3 worldPoint;
        Vector3 relativeVector;


        void Awake()
        {
            Initialize();
        }

        void LateUpdate()
        {
            LookAtViewCenterHandler();
        }

        void Initialize()
        {
            viewportCenter = new Vector3(0.5f, 0.5f, 0.0f);
        }

        void LookAtViewCenterHandler()
        {
            viewportCenter.z = camera.nearClipPlane;
            worldPoint = camera.ViewportToWorldPoint(viewportCenter);

            worldPoint += camera.transform.forward * 100.0f;

            relativeVector = (worldPoint - barrel.position);
            transform.rotation = Quaternion.LookRotation(relativeVector);
        }
    }
}

