using UnityEngine;
using Wheel.Control;
using Wheel.Reward;
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

        //[SerializeField] private Sprite[] wheelSprite;
        //[SerializeField] private Sprite[] pinSprite;
        #endregion

        [SerializeField] private UpperStripeGenerator upperStripe;
        protected override void Awake()
        {
            base.Awake();
            _wheelControl = wheelObject.GetComponent<WheelHandler>();
        }
        private void Start()
        {
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
        }
        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += NewRound;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= NewRound;
        }
        private void NewRound()
        {
            CurrentRound++;
        }
        public void NextRound()
        {
            upperStripe.MoveStripe();
            CurrentRound++;
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
            _wheelControl.RestartWheel();
        }


        #region Wheel Control Methods
        public void SpinWheel()
        {
            _wheelControl.StartSpinning();
            GameStateHandler.ChangeState(GameState.Spinning);
            
        }
        public void RestartWheel()
        {
            _wheelControl.RestartWheel();
        }
        #endregion
    }
}