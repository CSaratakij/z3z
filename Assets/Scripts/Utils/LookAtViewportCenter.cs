using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class LookAtViewportCenter : MonoBehaviour
    {
        [SerializeField]
        new Camera camera;

        [SerializeField]
        Transform barrel;

        Vector3 viewportCenter;
        Vector3 worldPoint;
        Vector3 relativeVector;

        void Awake()
        {
            Initialize();
        }

        void Start()
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

            worldPoint += camera.transform.forward * 1000.0f;

            relativeVector = (worldPoint - barrel.position);
            transform.rotation = Quaternion.LookRotation(relativeVector);
        }

        internal Vector3 DirectionToCenter()
        {
            return relativeVector.normalized;
        }
    }
}

