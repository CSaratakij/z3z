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

        enum View
        {
            GameOver
        }

        void Awake()
        {
            SubscribeEvent();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
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

        public void RestartGame()
        {
            ObjectiveChecker.ClearStat();
            GameController.Reset();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}

