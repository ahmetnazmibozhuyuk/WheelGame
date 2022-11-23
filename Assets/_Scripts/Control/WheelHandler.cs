﻿using UnityEngine;
using DG.Tweening;
using Wheel.Managers;

namespace Wheel.Control
{

    //spin bittiğinde bir kez daha çalışıp hangi kısım kazandıysa onun ortasına dönecek
    //spin olurken her segmenti geçtiğinde ses çıkacak

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
                return transform.rotation.eulerAngles.z % 360;
            }
        }

        private AngleLimit[] _angleLimits;
        private readonly int _segmentCount = 8;
        private readonly float _startAngle = 0;

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
                OnUpdate(() => transform.rotation = Quaternion.Euler(0, 0, _currentTurnAngle)).OnComplete(() => GameManager.Instance.ResultPhase(ResultIndex()));
        }
        private int ResultIndex()
        {
            GameStateHandler.ChangeState(GameState.SpinningFinished);

            for (int i = 0; i < _angleLimits.Length; i++)
            {
                if (_finalScore >= _angleLimits[i].minimumAngle && _finalScore <= _angleLimits[i].maximumAngle)
                {
                    Debug.Log("winner: " + i);



                    //kart ciktiginda da efektler vs cikacak

                    //once geri donup duzgun pozisyon alacak (bulunan reward segmentini ortasina donecek)


                    _rewardHandler.ActivateCard(i);
                    return i;
                    
                }
            }
            return 0;
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