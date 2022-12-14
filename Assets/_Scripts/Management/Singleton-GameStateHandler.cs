namespace Wheel.Managers
{
    using System;
    public static class GameStateHandler
    {
        public static GameState CurrentState { get; private set; }

        public static event Action OnGameAwaitingStartState;
        public static event Action OnSpinningState;
        public static event Action OnSpinningFinishedState;
        public static event Action OnGameLostState;
        public static event Action OnGameWonState;

        public static void ChangeState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;
            switch (newState)
            {
                case GameState.GameAwaitingStart:
                    OnGameAwaitingStartState?.Invoke();
                    break;
                case GameState.Spinning:
                    OnSpinningState?.Invoke();
                    break;
                case GameState.SpinningFinished:
                    OnSpinningFinishedState?.Invoke();
                    break;
                case GameState.GameLost:
                    OnGameLostState?.Invoke();
                    break;
                case GameState.GameWon:
                    OnGameWonState?.Invoke();
                    break;
                default:
                    throw new ArgumentException("Invalid game state selection.");
            }
        }
    }
    public enum GameState
    {
        GamePreStart = 0,
        GameAwaitingStart = 1,
        Spinning = 2,
        SpinningFinished = 3,
        GameLost = 4,
        GameWon = 5,
    }
}
namespace Wheel.Managers
{
    using UnityEngine;
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this as T;
        }
    }
}