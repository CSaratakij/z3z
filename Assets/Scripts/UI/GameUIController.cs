using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Z3Z
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] views;

        [SerializeField]
        CursorController cursorController;

        internal enum View
        {
            PauseMenu,
            GameOver
        }

        void Awake()
        {
            SubscribeEvent();
        }

        void Update()
        {
            InputHandler();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
            Time.timeScale = 1.0f;
        }

        void InputHandler()
        {
            if (Input.GetButtonDown("Cancel")) {
                TogglePauseMenu();
            }
        }

        void HideAllView()
        {
            for (int i = 0; i < views.Length; ++i)
            {
                Show(i, false);
            }
        }

        void Show(int id, bool isShow)
        {
            views[id].gameObject.SetActive(isShow);
        }

        void Show(View view, bool isShow)
        {
            Show((int)view, isShow);
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
            HideAllView();
        }

        void OnGameOver()
        {
            Show(View.GameOver, true);
        }

        public void TogglePauseMenu()
        {
            bool isShow = views[(int)View.PauseMenu].gameObject.activeSelf;
            isShow = !isShow;

            if (isShow) {
                cursorController.ShowCursor();
            }
            else {
                cursorController.LockCursor();
                GameSetting.Save();
            }

            GameController.Pause(isShow);
            Show(View.PauseMenu, isShow);
        }

        public void RestartGame()
        {
            ObjectiveChecker.ClearStat();
            GameController.Reset();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }

        public void BackToMainMenu()
        {
            ObjectiveChecker.ClearStat();
            GameController.Reset();
            SceneManager.LoadScene(0);
        }
    }
}

