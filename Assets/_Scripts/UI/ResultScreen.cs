using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wheel.Reward;

namespace Wheel.UI
{
    public class ResultScreen : MonoBehaviour
    {

        public void SetGainedText(RewardAttributes reward)
        {

        }
        private void AddToGained(RewardType rewardType)
        {
            switch (rewardType)
            {
                case RewardType.Money:
                    break;
                case RewardType.Gold:
                    break;
                case RewardType.CommonChest:
                    break;
                case RewardType.UncommonChest:
                    break;
                case RewardType.RareChest:
                    break;
                case RewardType.Grenade:
                    break;
                case RewardType.Knife:
                    break;
                case RewardType.Pistol:
                    break;
                case RewardType.Rifle:
                    break;
                case RewardType.Shotgun:
                    break;
                case RewardType.Sniper:
                    break;
                case RewardType.Helmet:
                    break;
                case RewardType.Pumpkin:
                    break;
                case RewardType.Glasses:
                    break;
                case RewardType.GrenadePoint:
                    break;
                case RewardType.KnifePoint:
                    break;
                case RewardType.PistolPoint:
                    break;
                case RewardType.SMGPoint:
                    break;
                case RewardType.RiflePoint:
                    break;
                case RewardType.ShotgunPoint:
                    break;
                case RewardType.SniperPoint:
                    break;
                case RewardType.ArmorPoint:
                    break;
            }
        }

    }
}
