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

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2)) {
                ShowCursor();
            }
        }
#endif

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void Initialize()
        {
            ShowCursor();
        }

        internal void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        internal void ShowCursor()
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

