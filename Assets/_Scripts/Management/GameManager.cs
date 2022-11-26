using UnityEngine;
using Wheel.Control;
using Wheel.UI;

namespace Wheel.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        #region Round Info

        public int CurrentRound { get; private set; }

        public int SafeRoundIndex { get { return safeRoundIndex; } }
        [SerializeField] private int safeRoundIndex = 5;
        public int SuperRoundIndex { get { return superRoundIndex; } }
        [SerializeField] private int superRoundIndex = 30;
        #endregion

        #region Wheel Related
        [SerializeField] private GameObject wheelObject;
        private WheelHandler _wheelControl;
        #endregion

        [SerializeField] private UpperStripeGenerator upperStripe;
        protected override void Awake()
        {
            base.Awake();
            _wheelControl = wheelObject.GetComponent<WheelHandler>();
            CurrentRound = 1;
        }
        public void StartGame()
        {
            /*
            Game should switch to awaiting start phase only when all async initial operations are complete.
            If we had more loading operations we'd wait for all of them to finish but since this case only
            has one async asset loading in the beginning we just need to start once it is finished loading.
            */
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
        }
        public void NextRound()
        {
            upperStripe.MoveStripe();
            CurrentRound++;
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
            _wheelControl.RestartWheel();
        }
        public void RestartRound()
        {
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
            _wheelControl.RestartWheel();
        }

        #region Wheel Control Methods
        public void SpinWheel()
        {
            _wheelControl.StartSpinning();
            GameStateHandler.ChangeState(GameState.Spinning);
            
        }
        #endregion
    }
}