using UnityEngine;
using DG.Tweening;
using Wheel.Managers;
using UnityEngine.UI;

namespace Wheel.Control
{
    [RequireComponent(typeof(RewardHandler))]
    public class WheelHandler : MonoBehaviour
    {
        private RewardHandler _rewardHandler;

        [SerializeField] private Image wheelImage;
        [SerializeField] private Image pinImage;

        [SerializeField] private Sprite[] wheelAlternativeSprite;
        [SerializeField] private Sprite[] pinAlternativeSprite;

        private float _currentTurnAngle;
        private float _finalScore
        {
            get
            {
                return transform.rotation.eulerAngles.z % 360+_offsetAngle;
            }
        }
        private AngleLimit[] _angleLimits;
        private readonly int _segmentCount = 8;
        private readonly float _offsetAngle = 45;

        private void Awake()
        {
            _rewardHandler = GetComponent<RewardHandler>();
        }
        private void Start()
        {
            InitializeAngles();
        }
        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += AssignSprite;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= AssignSprite;
        }
        private void InitializeAngles()
        {
            _angleLimits = new AngleLimit[_segmentCount];
            for (int i = 0; i < _angleLimits.Length; i++)
            {
                _angleLimits[i].minimumAngle = ((360 / _segmentCount) * i + _offsetAngle)  - 1;
                _angleLimits[i].maximumAngle = ((360 / _segmentCount) * (i + 1) + _offsetAngle) + 1;
            }
        }
        public void StartSpinning()
        {
            DOTween.To(() => _currentTurnAngle, x => _currentTurnAngle = x, 2000 + Random.Range(0f, 360f), 6 + Random.Range(0f, 5f)).
                SetEase(Ease.InOutCubic).
                OnUpdate(() => transform.rotation = Quaternion.Euler(0, 0, _currentTurnAngle)).OnComplete(() => ResultPhase());
        }
        private void ResultPhase()
        {
            GameStateHandler.ChangeState(GameState.SpinningFinished);
            for (int i = 0; i < _angleLimits.Length; i++)
            {

                if (_finalScore >= _angleLimits[i].minimumAngle && _finalScore <= _angleLimits[i].maximumAngle)
                {
                    if (_angleLimits[i].maximumAngle- _finalScore < _finalScore - _angleLimits[i].minimumAngle)
                    {
                        transform.DORotate(new Vector3(0, 0, (_angleLimits[i].minimumAngle)), 0.5f).OnComplete(() => { 
                            if(i==_segmentCount-1)
                                _rewardHandler.ActivateCard(0);
                            else
                                _rewardHandler.ActivateCard(i + 1);
                        });
                        return;
                    }
                    else
                    {
                        transform.DORotate(new Vector3(0, 0, (_angleLimits[i].minimumAngle - _offsetAngle)), 0.5f).OnComplete(() => _rewardHandler.ActivateCard(i));
                        return;
                    }
                }
            }

        }
        public void RestartWheel()
        {
            transform.rotation = Quaternion.identity;
            _currentTurnAngle = 0;
        }
        private void AssignSprite()
        {
            if (GameManager.Instance.CurrentRound % GameManager.Instance.SuperRoundIndex == 0)
            {
                pinImage.sprite = pinAlternativeSprite[2];
                wheelImage.sprite = wheelAlternativeSprite[2];
                return;
            }
            if (GameManager.Instance.CurrentRound % GameManager.Instance.SafeRoundIndex == 0)
            {
                pinImage.sprite = pinAlternativeSprite[1];
                wheelImage.sprite = wheelAlternativeSprite[1];
                return;
            }
            pinImage.sprite = pinAlternativeSprite[0];
            wheelImage.sprite = wheelAlternativeSprite[0];
        }
    }
    public struct AngleLimit
    {
        public float minimumAngle;
        public float maximumAngle;
    }
}