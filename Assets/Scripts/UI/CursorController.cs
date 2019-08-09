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
            SubscribeEvent();
        }

        void Update()
        {
            InputHandler();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void Initialize()
        {
            ShowCursor();
        }

        void InputHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Cursor.lockState = (CursorLockMode.None == Cursor.lockState) ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = (Cursor.lockState == CursorLockMode.None);
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F2)) {
                ShowCursor();
            }
#endif
        }

        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        void SubscribeEvent()
        {
            GameController.OnGameStart += OnGameStart;
            GameController.OnGameOver += OnGameOver;
        }

        void UnsubscribeEvent()
        {
            GameController.OnGameStart -= OnGameStart;
            GameController.OnGameOver -= OnGameOver;
        }

        void OnGameStart()
        {
            LockCursor();
        }

        void OnGameOver()
        {
            ShowCursor();
        }
    }
}

