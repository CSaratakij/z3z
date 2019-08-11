using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class UIMainMenu : MonoBehaviour
    {
        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            GameSetting.Load();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

