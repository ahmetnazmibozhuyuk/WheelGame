using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wheel.Reward;
using Wheel.Managers;

namespace Wheel.Control
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private Image[] rewardImages;

        [SerializeField] private RewardAttributes[] rewards;

        [SerializeField] private Sprite commonBGSprite;
        [SerializeField] private Sprite uncommonBGSprite;
        [SerializeField] private Sprite rareBGSprite;
        private Sprite _currentSprite;


        private List<RewardAttributes> _commonRewardList = new List<RewardAttributes>();
        private List<RewardAttributes> _uncommonRewardList = new List<RewardAttributes>();
        private List<RewardAttributes> _rareRewardList = new List<RewardAttributes>();

        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += InitializeRewardLists;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= InitializeRewardLists;
        }
        private void InitializeRewardLists()
        {
            _commonRewardList.Clear();
            _uncommonRewardList.Clear();
            _rareRewardList.Clear();
            for (int i = 0; i < rewards.Length; i++)
            {
                if (rewards[i].Rarity == RewardRarity.Common)
                {
                    _commonRewardList.Add(rewards[i]);
                    continue;
                }
                if (rewards[i].Rarity == RewardRarity.Uncommon)
                {
                    _uncommonRewardList.Add(rewards[i]);
                    continue;
                }
                if (rewards[i].Rarity == RewardRarity.Rare)
                {
                    _rareRewardList.Add(rewards[i]);
                    continue;
                }
            }
        }
        private void AssignRewards()
        {
            for (int i = 0; i < rewards.Length; i++)
            {
                rewardImages[i].sprite = rewards[i].RewardSprite;
            }
        }

    }
}
