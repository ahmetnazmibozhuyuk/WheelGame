﻿using UnityEngine;

namespace Wheel.Reward
{
    [CreateAssetMenu(fileName = "New Reward", menuName = "Scriptable Objects/New Reward", order = 1)]
    public class RewardAttributes : ScriptableObject
    {
        public string RewardName;
        public string RewardDescription;
        public int RewardAmount; // bu bi multiplier'a bağlı olacak giderek artacak

        public Sprite RewardSprite;

        public RewardRarity Rarity;
        [Range(0,20)]public float DropRate;
    }
    public enum RewardRarity { Common = 0, Uncommon = 1, Rare = 2 }
}
