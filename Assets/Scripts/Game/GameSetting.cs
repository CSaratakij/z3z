using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public static class GameSetting
    {
        internal static event Action OnSettingChanged;

        const string KEY_INITIALIZED = "K_Initialized";
        const string KEY_MOUSE_SENSITIVITY = "K_Mouse_Sensitivity";
        const string KEY_GAMEPAD_SENSITIVITY = "K_Gamepad_Sensitivity";

        internal static float MouseSensitivity { get; private set; }
        internal static float GamepadSensitivity { get; private set; }

        internal static void Initialize()
        {
            PlayerPrefs.SetFloat(KEY_MOUSE_SENSITIVITY, 1.0f);
            PlayerPrefs.SetFloat(KEY_GAMEPAD_SENSITIVITY, 2.0f);
            PlayerPrefs.SetInt(KEY_INITIALIZED, 1);
            PlayerPrefs.Save();
        }

        internal static void Save()
        {
            PlayerPrefs.SetFloat(KEY_MOUSE_SENSITIVITY, MouseSensitivity);
            PlayerPrefs.SetFloat(KEY_GAMEPAD_SENSITIVITY, GamepadSensitivity);
            OnSettingChanged?.Invoke();
        }

        internal static void Load()
        {
            bool isInitialized = PlayerPrefs.HasKey(KEY_INITIALIZED);

            if (!isInitialized)
                Initialize();

            MouseSensitivity = PlayerPrefs.GetFloat(KEY_MOUSE_SENSITIVITY, 1.0f);
            GamepadSensitivity = PlayerPrefs.GetFloat(KEY_GAMEPAD_SENSITIVITY, 2.0f);
        }

        internal static void SetMouseSensitivity(float value)
        {
            MouseSensitivity = value;
        }

        internal static void SetGamepadSensitivity(float value)
        {
            GamepadSensitivity = value;
        }
    }
}

