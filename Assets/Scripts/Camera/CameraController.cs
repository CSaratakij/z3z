using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class CameraController : MonoBehaviour
    {
        const float MAX_ANGLE_X = 80.0f;
        const float MAX_ANGLE_Y = 360.0f;

        [SerializeField]
        [Range(1.0f, 10.0f)]
        float mouseSensitivity;

        [SerializeField]
        [Range(1.0f, 10.0f)]
        float gamepadSensitivity;

        [SerializeField]
        [Range(0.1f, 1.0f)]
        float smoothDamp;

        [SerializeField]
        Transform target;

        [SerializeField]
        Vector3 offset;

        float sensitivity;

        Vector2 mouseAxis;
        Vector2 gamepadAxis;
        Vector2 inputAxis;

        Vector3 rotationAxis;

        void Awake()
        {
            Initialize();
            SubscribeEvent();
        }

        void Update()
        {
            InputHandler();
        }

        void LateUpdate()
        {
            FollowTargetHandler();
            RotateHandler();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void Initialize()
        {
            mouseSensitivity = GameSetting.MouseSensitivity;
            gamepadSensitivity = GameSetting.GamepadSensitivity;
        }

        void InputHandler()
        {
            if (!GameController.IsGameStart || GameController.IsGamePause)
                return;

            mouseAxis.x = Input.GetAxisRaw("Mouse X");
            mouseAxis.y = Input.GetAxisRaw("Mouse Y");

            gamepadAxis.x = Input.GetAxisRaw("GamePad X");
            gamepadAxis.y = Input.GetAxisRaw("GamePad Y");

            inputAxis = (GamepadWatcher.IsReceiveAnyInput) ? gamepadAxis : mouseAxis;
            sensitivity = (GamepadWatcher.IsReceiveAnyInput) ? gamepadSensitivity : mouseSensitivity;

            rotationAxis.x += (-inputAxis.y * sensitivity);
            rotationAxis.x = Mathf.Clamp(rotationAxis.x, -MAX_ANGLE_X, MAX_ANGLE_X);

            rotationAxis.y += (inputAxis.x * sensitivity);

            if (rotationAxis.y > MAX_ANGLE_Y) {
                rotationAxis.y = rotationAxis.y - MAX_ANGLE_Y;
            }
            else if (rotationAxis.y < -MAX_ANGLE_Y) {
                rotationAxis.y = MAX_ANGLE_Y + rotationAxis.y;
            }
        }

        void FollowTargetHandler()
        {
            transform.position = target.position + offset;
        }

        void RotateHandler()
        {
            RotateCamera();
            RotateTarget();
        }

        void RotateCamera()
        {
            Quaternion targetRotation = Quaternion.Euler(rotationAxis.x, rotationAxis.y, 0.0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothDamp);
        }

        void RotateTarget()
        {
            Vector3 lookVector = transform.forward;
            lookVector.y = 0.0f;
            target.localRotation = Quaternion.LookRotation(lookVector, Vector3.up);
        }

        void SubscribeEvent()
        {
            GameSetting.OnSettingChanged += OnSettingChanged;
        }

        void UnsubscribeEvent()
        {
            GameSetting.OnSettingChanged -= OnSettingChanged;
        }

        void OnSettingChanged()
        {
            mouseSensitivity = GameSetting.MouseSensitivity;
            gamepadSensitivity = GameSetting.GamepadSensitivity;
        }
    }
}

