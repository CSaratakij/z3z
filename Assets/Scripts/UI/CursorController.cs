using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField]
        CursorLockMode cursorLockState;


        void Awake()
        {
            Initialize();
        }

        void Update()
        {
            InputHandler();
        }

        void Initialize()
        {
            Cursor.lockState = cursorLockState;
            Cursor.visible = false;
        }

        void InputHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Cursor.lockState = (CursorLockMode.None == Cursor.lockState) ? CursorLockMode.Locked : CursorLockMode.None;
            }
        }
    }
}
