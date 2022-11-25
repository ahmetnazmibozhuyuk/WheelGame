using DG.Tweening;
using TMPro;
using UnityEngine;
using Wheel.Managers;
using Wheel.Reward;

namespace Wheel.UI
{
    public class ResultScreen : MonoBehaviour
    {
        private TextMeshProUGUI[] _resultText = new TextMeshProUGUI[22];
        private int[] _resultValue = new int[22];

        //[SerializeField] private TextMeshProUGUI moneyText;
        //[SerializeField] private TextMeshProUGUI goldText;
        //[SerializeField] private TextMeshProUGUI commonChestText;
        //[SerializeField] private TextMeshProUGUI uncommonChestText;
        //[SerializeField] private TextMeshProUGUI rareChestText;
        //[SerializeField] private TextMeshProUGUI grenadeText;
        //[SerializeField] private TextMeshProUGUI knifeText;
        //[SerializeField] private TextMeshProUGUI pistolText;
        //[SerializeField] private TextMeshProUGUI rifleText;
        //[SerializeField] private TextMeshProUGUI shotgunText;
        //[SerializeField] private TextMeshProUGUI sniperText;
        //[SerializeField] private TextMeshProUGUI helmetText;
        //[SerializeField] private TextMeshProUGUI pumpkinText;
        //[SerializeField] private TextMeshProUGUI glassesText;
        //[SerializeField] private TextMeshProUGUI grenadePointText;
        //[SerializeField] private TextMeshProUGUI knifePointText;
        //[SerializeField] private TextMeshProUGUI pistolPointText;
        //[SerializeField] private TextMeshProUGUI smgPointText;
        //[SerializeField] private TextMeshProUGUI riflePointText;
        //[SerializeField] private TextMeshProUGUI shotgunPointText;
        //[SerializeField] private TextMeshProUGUI sniperPointText;
        //[SerializeField] private TextMeshProUGUI armorPointText;

        private void Awake()
        {
            for(int i = 0; i < _resultText.Length; i++)
            {
                _resultText[i] = transform.GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>();
            }
        }
        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += InitializePanel;
            GameStateHandler.OnGameWonState += Activate;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= InitializePanel;
            GameStateHandler.OnGameWonState -= Activate;
        }
        private void InitializePanel()
        {
            transform.localScale = Vector3.zero;
        }
        private void Activate()
        {
            transform.DOScale(Vector3.one, 0.5f);
        }


        public void SetGainedText(RewardAttributes reward)
        {
            AddToGained(reward.Type, reward.RewardAmount);
        }
        private void AddToGained(RewardType rewardType, int rewardAmount)
        {
            switch (rewardType)
            {
                case RewardType.Money:
                    _resultValue[0] += rewardAmount;
                    _resultText[0].SetText("Money: " + _resultValue[0]);
                    break;
                case RewardType.Gold:
                    _resultValue[1] += rewardAmount;
                    _resultText[1].SetText("Gold: " + _resultValue[1]);
                    break;
                case RewardType.CommonChest:
                    _resultValue[2] += rewardAmount;
                    _resultText[2].SetText("Common Chest: " + _resultValue[2]);
                    break;
                case RewardType.UncommonChest:
                    _resultValue[3] += rewardAmount;
                    _resultText[3].SetText("Uncommon Chest: " + _resultValue[3]);
                    break;
                case RewardType.RareChest:
                    _resultValue[4] += rewardAmount;
                    _resultText[4].SetText("Rare Chest: " + _resultValue[4]);
                    break;
                case RewardType.Grenade:
                    _resultValue[5] += rewardAmount;
                    _resultText[5].SetText("Grenade: " + _resultValue[5]);
                    break;
                case RewardType.Knife:
                    _resultValue[6] += rewardAmount;
                    _resultText[6].SetText("Knife: " + _resultValue[6]);
                    break;
                case RewardType.Pistol:
                    _resultValue[7] += rewardAmount;
                    _resultText[7].SetText("Pistol: " + _resultValue[7]);
                    break;
                case RewardType.Rifle:
                    _resultValue[8] += rewardAmount;
                    _resultText[8].SetText("Rifle: " + _resultValue[8]);
                    break;
                case RewardType.Shotgun:
                    _resultValue[9] += rewardAmount;
                    _resultText[9].SetText("Shotgun: " + _resultValue[9]);
                    break;
                case RewardType.Sniper:
                    _resultValue[10] += rewardAmount;
                    _resultText[10].SetText("Sniper: " + _resultValue[10]);
                    break;
                case RewardType.Helmet:
                    _resultValue[11] += rewardAmount;
                    _resultText[11].SetText("Helmet: " + _resultValue[11]);
                    break;
                case RewardType.Pumpkin:
                    _resultValue[12] += rewardAmount;
                    _resultText[12].SetText("Pumpkin: " + _resultValue[12]);
                    break;
                case RewardType.Glasses:
                    _resultValue[13] += rewardAmount;
                    _resultText[13].SetText("Glasses: " + _resultValue[13]);
                    break;
                case RewardType.GrenadePoint:
                    _resultValue[14] += rewardAmount;
                    _resultText[14].SetText("Grenade Point: " + _resultValue[14]);
                    break;
                case RewardType.KnifePoint:
                    _resultValue[15] += rewardAmount;
                    _resultText[15].SetText("Knife Point: " + _resultValue[15]);
                    break;
                case RewardType.PistolPoint:
                    _resultValue[16] += rewardAmount;
                    _resultText[16].SetText("Pistol Point: " + _resultValue[16]);
                    break;
                case RewardType.SMGPoint:
                    _resultValue[17] += rewardAmount;
                    _resultText[17].SetText("SMG Point: " + _resultValue[17]);
                    break;
                case RewardType.RiflePoint:
                    _resultValue[18] += rewardAmount;
                    _resultText[18].SetText("Rifle Point: " + _resultValue[18]);
                    break;
                case RewardType.ShotgunPoint:
                    _resultValue[19] += rewardAmount;
                    _resultText[19].SetText("Shotgun Point: " + _resultValue[19]);
                    break;
                case RewardType.SniperPoint:
                    _resultValue[20] += rewardAmount;
                    _resultText[20].SetText("Sniper Point: " + _resultValue[20]);
                    break;
                case RewardType.ArmorPoint:
                    _resultValue[21] += rewardAmount;
                    _resultText[21].SetText("Armor Point: " + _resultValue[21]);
                    break;
            }
        }

    }
}
