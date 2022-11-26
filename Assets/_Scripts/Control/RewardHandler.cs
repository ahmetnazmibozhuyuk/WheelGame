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

        [SerializeField] private RewardAttributes deathReward;
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
            _selectedRewardsList.Clear();
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
            if(GameManager.Instance.CurrentRound%GameManager.Instance.SuperRoundIndex == 0)
            {
                GoldenRewards();
                return;
            }
            if (GameManager.Instance.CurrentRound % GameManager.Instance.SafeRoundIndex == 0)
            {
                SilverRewards();
                return;
            }
            BronzeReward();
        }
        private void BronzeReward()
        {

            _selectedRewardsList.Add(deathReward);
            rewardImages[0].sprite = deathReward.RewardSprite;
            rewardImages[0].SetNativeSize();
            for (int k = 1; k < rewardImages.Length; k++)
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
                            _rewardLists[i].RemoveAt(j);
                            break;
                        }
                    }
                    if (chosenReward != null) break;
                }
                if (chosenReward == null)
                {
                    int randomIndex = Random.Range(0, _commonRewardList.Count);
                    chosenReward = _commonRewardList[randomIndex];
                    _commonRewardList.RemoveAt(randomIndex);
                }
                _selectedRewardsList.Add(chosenReward);
                rewardImages[k].sprite = chosenReward.RewardSprite;
                rewardImages[k].SetNativeSize();
            }
        }
        private void SilverRewards()
        {
            for (int k =0; k < rewardImages.Length; k++)
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
                            _rewardLists[i].RemoveAt(j);
                            break;
                        }
                    }
                    if (chosenReward != null) break;
                }
                if (chosenReward == null)
                {
                    int randomIndex = Random.Range(0, _commonRewardList.Count);
                    chosenReward = _commonRewardList[randomIndex];
                    _commonRewardList.RemoveAt(randomIndex);
                }
                _selectedRewardsList.Add(chosenReward);
                rewardImages[k].sprite = chosenReward.RewardSprite;
                rewardImages[k].SetNativeSize();
            }
        }
        private void GoldenRewards()
        {
            for (int k = 0; k < rewardImages.Length; k++)
            {
                RewardAttributes chosenReward = null;

                    for (int j = 0; j < _rareRewardList.Count; j++)
                    {
                        float randomRoll = Random.Range(0f, 100f);
                        if (_rareRewardList[j].DropRate > randomRoll)
                        {
                            chosenReward = _rareRewardList[j];
                        _rareRewardList.RemoveAt(j);
                            break;
                        }
                    }
                    if (chosenReward != null) break;
                
                if (chosenReward == null)
                {
                    int randomIndex = Random.Range(0, _rareRewardList.Count);
                    chosenReward = _rareRewardList[randomIndex];
                    _rareRewardList.RemoveAt(randomIndex);
                }
                _selectedRewardsList.Add(chosenReward);
                rewardImages[k].sprite = chosenReward.RewardSprite;
                //rewardImages[k].sprite = SpriteManager.Instance.GetRewardSprite(chosenReward.SpriteName);
                rewardImages[k].SetNativeSize();
            }
        }
        public void ActivateCard(int rewardIndex)
        {
            rewardCard.DisplayCard(_selectedRewardsList[rewardIndex]);
            GameStateHandler.ChangeState(GameState.SpinningFinished);
        }


    }
}
