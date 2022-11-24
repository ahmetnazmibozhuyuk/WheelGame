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



        [SerializeField]private int minimumCommonItemRound = 0;
        [SerializeField] private int maximumCommonItemRound = 15;

        [SerializeField] private int minimumUncommonItemRound = 3;
        [SerializeField] private int maximumUncommonItemRound = 50;

        [SerializeField] private int minimumRareItemRound = 12;
        [SerializeField] private int maximumRareItemRound = 150;


        private List<RewardAttributes> _commonRewardList = new List<RewardAttributes>();
        private List<RewardAttributes> _uncommonRewardList = new List<RewardAttributes>();
        private List<RewardAttributes> _rareRewardList = new List<RewardAttributes>();

        private List<List<RewardAttributes>> _rewardLists = new List<List<RewardAttributes>>();

        private List<RewardAttributes> _selectedRewardsList = new List<RewardAttributes>();

        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += InitializeRewards;

        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= InitializeRewards;
        }
        private void InitializeRewards()
        {
            _commonRewardList.Clear();
            _uncommonRewardList.Clear();
            _rareRewardList.Clear();
            _rewardLists.Clear();
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
            if (GameManager.Instance.CurrentRound >= minimumCommonItemRound && GameManager.Instance.CurrentRound < maximumCommonItemRound)
            {
                _rewardLists.Add(_commonRewardList);
            }
            if (GameManager.Instance.CurrentRound >= minimumUncommonItemRound && GameManager.Instance.CurrentRound < maximumUncommonItemRound)
            {
                _rewardLists.Add(_uncommonRewardList);
            }
            if (GameManager.Instance.CurrentRound >= minimumRareItemRound && GameManager.Instance.CurrentRound < maximumRareItemRound)
            {
                _rewardLists.Add(_rareRewardList);
            }
            AssignRewards();

        }
        private void AssignRewards()
        {
            for(int k = 0; k < rewardImages.Length; k++)
            {
                RewardAttributes chosenReward = null;


                for (int i = 0; i < _rewardLists.Count; i++)
                {
                    for (int j = 0; j < _rewardLists[i].Count; j++)
                    {
                        float randomRoll = Random.Range(0f, 100f);
                        if (_rewardLists[i][j].DropRate > randomRoll)
                        {
                            chosenReward = _rewardLists[i][j];
                            break;
                        }
                    }
                    if (chosenReward != null) break;
                }
                if (chosenReward == null)
                {
                    chosenReward = _commonRewardList[Random.Range(0, _commonRewardList.Count)];
                }
                _selectedRewardsList.Add(chosenReward);
                rewardImages[k].sprite = chosenReward.RewardSprite;
                rewardImages[k].SetNativeSize();
            }

        }
        public void ActivateCard(int rewardIndex)
        {
            rewardCard.DisplayCard(SelectedBGSprite(_selectedRewardsList[rewardIndex].Rarity), _selectedRewardsList[rewardIndex].RewardSprite,
                _selectedRewardsList[rewardIndex].RewardName, _selectedRewardsList[rewardIndex].RewardAmount.ToString());
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
