using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z3Z
{
    public class UIAccuracyStat : MonoBehaviour
    {
        const string FORMAT = "Accuracy : {0:0.##}%";


        [SerializeField]
        Text txtAccuracy;

        [SerializeField]
        ObjectiveChecker objectiveChecker;


        void OnEnable()
        {
            UpdateUI();
        }

        void UpdateUI()
        {
            txtAccuracy.text = string.Format(FORMAT, objectiveChecker.Accuracy * 100.0f);
        }
    }
}

