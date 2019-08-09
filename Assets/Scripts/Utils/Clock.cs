using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Z3Z
{
    public class Clock : MonoBehaviour
    {
        public event Action OnBeginTick;
        public event Action OnEndTick;

        public float Elapsed {
            get {
                if (tickState == TickState.Begin) {
                    return Time.time - startSeconds;
                }
                else {
                    return endSeconds - startSeconds;
                }
            }
        }

        public bool IsTick => (tickState == TickState.Begin);

        enum TickState
        {
            Begin,
            End
        }

        float startSeconds;
        float endSeconds;

        TickState tickState = TickState.End;


        void Awake()
        {
            SubscribeEvent();
        }

        void OnDestroy()
        {
            UnsubscribeEvent();
        }

        void SubscribeEvent()
        {
            GameController.OnGameStart += OnGameStart;
            GameController.OnGameOver += OnGameOver;
            GameController.OnGameReset += OnGameReset;
        }

        void UnsubscribeEvent()
        {
            GameController.OnGameStart -= OnGameStart;
            GameController.OnGameOver -= OnGameOver;
            GameController.OnGameReset -= OnGameReset;

            OnBeginTick = null;
            OnEndTick = null;
        }

        void OnGameStart()
        {
            BeginTick();
        }

        void OnGameOver()
        {
            EndTick();
        }

        void OnGameReset()
        {
            startSeconds = 0.0f;
            endSeconds = 0.0f;
            tickState = TickState.End;
        }

        public void BeginTick()
        {
            if (tickState == TickState.Begin)
                return;

            tickState = TickState.Begin;
            startSeconds = Time.time;

            OnBeginTick?.Invoke();
        }

        public void EndTick()
        {
            if (tickState == TickState.End)
                return;

            tickState = TickState.End;
            endSeconds = Time.time;

            OnEndTick?.Invoke();
        }
    }
}

