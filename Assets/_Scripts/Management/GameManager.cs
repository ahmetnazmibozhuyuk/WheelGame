using UnityEngine;
using Wheel.Control;
using Wheel.Reward;

namespace Wheel.Managers
{
    [RequireComponent(typeof(UIManager))]
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
        public int RewardCount { get { return rewards.Length; } }
        [SerializeField] private RewardAttributes[] rewards = new RewardAttributes[7];
        [SerializeField] private Sprite[] wheelSprite;
        [SerializeField] private Sprite[] pinSprite;
        #endregion

        private UIManager _uiManager;

        protected override void Awake()
        {
            base.Awake();
            _uiManager = GetComponent<UIManager>();
            _wheelControl = wheelObject.GetComponent<WheelHandler>();
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



        #region Wheel Control Methods
        public void SpinWheel()
        {
            _wheelControl.StartSpinning();
        }
        public void RestartWheel()
        {
            _wheelControl.RestartWheel();
        }
        public void ResultPhase(int rewardIndex)
        {

        }
        #endregion
    }
}