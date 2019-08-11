using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z3Z
{
    public class PauseUIController : MonoBehaviour
    {
        [SerializeField]
        Text mouseSensitivityText;

        [SerializeField]
        Slider mouseSensitivitySlider;

        void Awake()
        {
            mouseSensitivitySlider.onValueChanged.AddListener((value) => {
                GameSetting.SetMouseSensitivity(value);
                UpdateUI(value);
            });
        }

        void OnEnable()
        {
            UpdateUI(GameSetting.MouseSensitivity);
        }

        void UpdateUI(float value)
        {
            mouseSensitivitySlider.value = value;
            mouseSensitivityText.text = value.ToString();
        }
    }
}

