using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wheel.Reward;
using Wheel.Managers;

namespace Wheel.Control
{
    public class RewardHandler : MonoBehaviour
    {
        [SerializeField] private CardHandler rewardCard;
        [SerializeField] private Image[] rewardImages;

        [SerializeField] private RewardAttributes[] rewards;

        [SerializeField] private Sprite commonBGSprite;
        [SerializeField] private Sprite uncommonBGSprite;
        [SerializeField] private Sprite rareBGSprite;



        private Sprite _acquiredBGSprite;
        private Sprite _acquiredRewardSprite;

        private List<RewardAttributes> _commonRewardList = new List<RewardAttributes>();
        private List<RewardAttributes> _uncommonRewardList = new List<RewardAttributes>();
        private List<RewardAttributes> _rareRewardList = new List<RewardAttributes>();

        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += InitializeRewardLists;
            GameStateHandler.OnGameAwaitingStartState += AssignRewards;
            //GameStateHandler.OnSpinningFinishedState += ActivateCard;

        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= InitializeRewardLists;
            GameStateHandler.OnGameAwaitingStartState -= AssignRewards;
            //GameStateHandler.OnSpinningFinishedState -= ActivateCard;
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
            for (int i = 0; i < rewardImages.Length; i++)
            {


                //random seçilecek, seçilen listeden dusecek
                //rarity ve verilen sayi seviye arttikca ve ozel bolumlerde artacak
                //biri garanti death olacak - özel bölüm değilse



                rewardImages[i].sprite = rewards[i].RewardSprite;
            }
        }
        public void ActivateCard(int rewardIndex)
        {
            rewardCard.DisplayCard(SelectedBGSprite(rewards[rewardIndex].Rarity), rewards[rewardIndex].RewardSprite, 
                rewards[rewardIndex].RewardName, rewards[rewardIndex].RewardAmount.ToString());
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
