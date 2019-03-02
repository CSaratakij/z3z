namespace Z3Z
{
    public class GameController
    {
        public delegate void _Func();

        public static event _Func OnGameStart;
        public static event _Func OnGameOver;

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
    }
}

