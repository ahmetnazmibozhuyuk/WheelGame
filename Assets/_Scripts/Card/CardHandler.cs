using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Wheel.Managers;
using Wheel.UI;

namespace Wheel.Reward
{
    public class CardHandler : MonoBehaviour
    {
        [SerializeField] private ResultScreen resultScreen;

        [SerializeField] private Image bgImage;
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI amountText;

        [SerializeField] private Sprite commonBGSprite;
        [SerializeField] private Sprite uncommonBGSprite;
        [SerializeField] private Sprite rareBGSprite;

        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialPosition = transform.position;
        }

        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += InitializeCard;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= InitializeCard;
        }
        private void InitializeCard()
        {
            transform.localScale = Vector3.zero;

        }

        public void DisplayCard(RewardAttributes earnedReward)
        {
            bgImage.sprite = SelectedBGSprite(earnedReward.Rarity);
            rewardImage.sprite = earnedReward.RewardSprite;
            nameText.SetText(earnedReward.RewardName);
            amountText.SetText("x"+earnedReward.RewardAmount);
            rewardImage.SetNativeSize();
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce).OnComplete(()=> ReapReward(earnedReward));
            resultScreen.SetGainedText(earnedReward);
        }
        private void ReapReward(RewardAttributes earnedReward)
        {
            if(earnedReward.Type == RewardType.Death)
            {
                GameStateHandler.ChangeState(GameState.GameLost);
            }
        }
        private Sprite SelectedBGSprite(RewardRarity rarity)
        {
            switch (rarity)
            {
                case RewardRarity.Common:
                    return commonBGSprite;
                case RewardRarity.Uncommon:
                    return uncommonBGSprite;
                case RewardRarity.Rare:
                    return rareBGSprite;
                default:
                    return commonBGSprite;
            }
        }
    }
}
