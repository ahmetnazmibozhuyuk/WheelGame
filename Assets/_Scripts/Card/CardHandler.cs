using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Wheel.Managers;

namespace Wheel.Reward
{
    public class CardHandler : MonoBehaviour
    {
        [SerializeField] private Image bgImage;
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI amountText;

        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += DismissCard;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= DismissCard;
        }
        private void DismissCard()
        {
            transform.localScale= Vector3.zero;
        }

        public void DisplayCard(Sprite bgSprite, Sprite rewardSprite, string nameString, string amountString)
        {
            bgImage.sprite = bgSprite;
            rewardImage.sprite = rewardSprite;
            nameText.SetText(nameString);
            amountText.SetText(amountString);
            transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);

        }

    }
}
