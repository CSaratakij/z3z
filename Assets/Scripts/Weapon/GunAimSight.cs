using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class GunAimSight : MonoBehaviour
    {
        [SerializeField]
        Transform gunModel;

        [SerializeField]
        float toggleDamp;

        [SerializeField]
        Vector3 normalPos;

        [SerializeField]
        Vector3 aimSightPos;


        enum AimState
        {
            Hip,
            Aim
        }

        Vector3 currentPos;

        AimState previousAimState;
        AimState aimState;


        void LateUpdate()
        {
            if (previousAimState == aimState)
                return;

            switch (aimState)
            {
                case AimState.Hip:
                    gunModel.localPosition = Vector3.Lerp(gunModel.localPosition, normalPos, toggleDamp);
                    break;

                case AimState.Aim:
                    gunModel.localPosition = Vector3.Lerp(gunModel.localPosition, aimSightPos, toggleDamp);
                    break;

                default:
                    break;
            }
        }

        public void ToggleSight()
        {
            previousAimState = aimState;
            aimState = (aimState == AimState.Hip) ? AimState.Aim : AimState.Hip;
        }

        public void Aim(bool value)
        {
            if (value) {
                if (aimState == AimState.Hip) {
                    previousAimState = aimState;
                    aimState = AimState.Aim;
                }
            }
            else {
                if (aimState == AimState.Aim) {
                    previousAimState = aimState;
                    aimState = AimState.Hip;
                }
            }
        }
    }
}

