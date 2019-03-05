using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class GamepadWatcher : MonoBehaviour
    {
        static bool previousConnectState = false;

        public static bool IsGamepadConnected = false;
        public static bool IsReceiveAnyInput = false;
        public static GamepadWatcher Instance = null;


        [SerializeField]
        bool lessFrequent = false;

        [SerializeField]
        string[] virtualAxisName = new string[2];


        void Awake()
        {
            MakeSingleton();
        }
        
        void Start()
        {
            CheckReceiveAnyInput();
        }

        void Update()
        {
            PollingRateHandler();
        }

        void MakeSingleton()
        {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

        void PollingRateHandler()
        {
            if (lessFrequent) {
                if (Input.anyKeyDown) {
                    CheckReceiveAnyInput();
                }
            }
            else {
                CheckReceiveAnyInput();
            }
        }

        void CheckConnectedGamepad()
        {
            foreach (string name in Input.GetJoystickNames()) {
                if (string.IsNullOrEmpty(name)) {
                    previousConnectState = IsGamepadConnected;
                    IsGamepadConnected = false;
                    continue;
                }
                else {
                    previousConnectState = IsGamepadConnected;
                    IsGamepadConnected = true;
                    break;
                }
            }

            if (previousConnectState != IsGamepadConnected) {
                Debug.Log((IsGamepadConnected) ? "Gamepad Connected" : "Gamepad Disconnect");
            }
        }

        void CheckReceiveAnyInput()
        {
            Vector2 gamepadAxis;

            gamepadAxis.x = Input.GetAxis(virtualAxisName[0]);
            gamepadAxis.y = Input.GetAxis(virtualAxisName[1]);

            IsReceiveAnyInput = (gamepadAxis.magnitude > 0.1f) ? true : false;
        }
    }
}

