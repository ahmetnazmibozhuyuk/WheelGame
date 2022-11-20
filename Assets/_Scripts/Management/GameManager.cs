using UnityEngine;
using Wheel.Control;
using Wheel.Reward;

namespace Wheel.Managers
{
    [RequireComponent(typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        #region Wheel Related
        [SerializeField] private GameObject wheelObject;
        private WheelHandler _wheelControl;
        public int RewardCount { get { return rewards.Length; } }

        [SerializeField] private RewardAttributes[] rewards = new RewardAttributes[7];

        #endregion

        private UIManager _uiManager;

        protected override void Awake()
        {
            base.Awake();
            _uiManager = GetComponent<UIManager>();
            _wheelControl = wheelObject.GetComponent<WheelHandler>();
        }
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
    }
}