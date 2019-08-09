using System;

namespace Z3Z
{
    public class GameController
    {
        public static event Action OnGameStart;
        public static event Action OnGameOver;
        public static event Action OnGameReset;

        public static bool IsGameStart { get; private set; }


        public static void GameStart()
        {
            if (IsGameStart)
                return;

            IsGameStart = true;
            OnGameStart?.Invoke();
        }

        public static void GameOver()
        {
            if (!IsGameStart)
                return;

            IsGameStart = false;
            OnGameOver?.Invoke();
        }

        public static void Reset()
        {
            IsGameStart = false;
            OnGameReset?.Invoke();
        }
    }
}

