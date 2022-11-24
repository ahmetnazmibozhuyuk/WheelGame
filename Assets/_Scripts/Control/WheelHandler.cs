using UnityEngine;
using DG.Tweening;
using Wheel.Managers;

namespace Wheel.Control
{


    //dönerken her segmenti geçtiğinde ses çıkacak ve pin hareketi

    [RequireComponent(typeof(RewardHandler))]
    public class WheelHandler : MonoBehaviour
    {
        private RewardHandler _rewardHandler;

        [SerializeField] private Sprite[] wheelSprite;
        [SerializeField] private Sprite[] pinSprite;

        private float _currentTurnAngle;
        private float _finalScore
        {
            get
            {

                return transform.rotation.eulerAngles.z % 360+_startAngle;
            }
        }

        private AngleLimit[] _angleLimits;
        private readonly int _segmentCount = 8;
        private readonly float _startAngle = 45;

        private void Awake()
        {
            _rewardHandler = GetComponent<RewardHandler>();
        }
        private void Start()
        {
            InitializeAngles();
        }
        private void InitializeAngles()
        {
            _angleLimits = new AngleLimit[_segmentCount];
            for (int i = 0; i < _angleLimits.Length; i++)
            {
                _angleLimits[i].minimumAngle = ((360 / _segmentCount) * i + _startAngle) % 360 - 1;
                _angleLimits[i].maximumAngle = ((360 / _segmentCount) * (i + 1) + _startAngle) % 360 + 1;
            }
        }
        public void StartSpinning()
        {
            DOTween.To(() => _currentTurnAngle, x => _currentTurnAngle = x, 2000 + Random.Range(0f, 360f), 6 + Random.Range(0f, 5f)).
                SetEase(Ease.InOutCubic).
                OnUpdate(() => transform.rotation = Quaternion.Euler(0, 0, _currentTurnAngle)).OnComplete(() => ResultIndex());
        }
        private void ResultIndex()
        {
            GameStateHandler.ChangeState(GameState.SpinningFinished);
            for (int i = 0; i < _angleLimits.Length; i++)
            {
                if (_finalScore >= _angleLimits[i].minimumAngle && _finalScore <= _angleLimits[i].maximumAngle)
                {
                    if (_angleLimits[i].maximumAngle-_currentTurnAngle < _currentTurnAngle - _angleLimits[i].minimumAngle)
                    {
                        transform.DORotate(new Vector3(0, 0, (_angleLimits[i].minimumAngle)), 0.5f).OnComplete(() => _rewardHandler.ActivateCard(i+1));
                        return;
                    }
                    else
                    {
                        transform.DORotate(new Vector3(0, 0, (_angleLimits[i].minimumAngle - _startAngle)), 0.5f).OnComplete(() => _rewardHandler.ActivateCard(i));
                        return;
                    }
                }
            }
            if (_finalScore >= _angleLimits[7].minimumAngle && _finalScore <= _angleLimits[7].maximumAngle)
            {
                transform.DORotate(new Vector3(0, 0, (_angleLimits[7].minimumAngle)), 0.5f).OnComplete(() => _rewardHandler.ActivateCard(0));
                return;
            }
            else
            {
                transform.DORotate(new Vector3(0, 0, (_angleLimits[7].minimumAngle - _startAngle)), 0.5f).OnComplete(() => _rewardHandler.ActivateCard(7));
                return;
            }
        }
        public void RestartWheel()
        {
            transform.rotation = Quaternion.identity;
            _currentTurnAngle = 0;
        }
    }
    public struct AngleLimit
    {
        public float minimumAngle;
        public float maximumAngle;
    }
}