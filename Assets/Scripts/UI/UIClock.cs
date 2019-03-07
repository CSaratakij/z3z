using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Z3Z
{
    public class UIClock : MonoBehaviour
    {
        const string FORMAT = "Time : {0:0.##} sec";

        [SerializeField]
        Text lblTimer;

        [SerializeField]
        Clock clock;


        void OnEnable()
        {
            UpdateUI();
        }

        void UpdateUI()
        {
            lblTimer.text = string.Format(FORMAT, clock.Elapsed);
        }
    }
}
