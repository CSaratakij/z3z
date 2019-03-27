using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z3Z
{
    public class UIObjectiveResult : MonoBehaviour
    {
        [SerializeField]
        Text lblPass;

        [SerializeField]
        Color textColorPass;

        [SerializeField]
        Color textColorFail;

        [SerializeField]
        ObjectiveChecker objectiveChecker;

        [SerializeField]
        GameObject btnNextScene;


        void OnEnable()
        {
            UpdateUI();
        }

        void UpdateUI()
        {
            bool isPass = objectiveChecker.IsPassObjective();

            string message = (isPass) ? "Pass" : "Fail";
            Color textColor = (isPass) ? textColorPass : textColorFail;

            lblPass.text = message;
            lblPass.color = textColor;

            btnNextScene.gameObject.SetActive(isPass);
        }
    }
}

