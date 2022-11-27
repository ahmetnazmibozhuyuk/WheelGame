using UnityEngine;

namespace Wheel.Reward
{
    [CreateAssetMenu(fileName = "New Reward", menuName = "Scriptable Objects/New Reward", order = 1)]
    public class RewardAttributes : ScriptableObject
    {
        /*
         Scriptable Object rewards can be found in "Rewards - Scriptable" folder in Assets folder. The scriptable
        objects can be easily expanded, changed or created from editor without touching any code.
         */

        public string RewardName;
        public string RewardDescription;
        public int RewardAmount;

        public Sprite RewardSprite;

        public RewardRarity Rarity;
        public RewardType Type;
        [Range(0,20)]public float DropRate;

        public string SpriteName;
    }
    public enum RewardType { Death,Money,Gold,CommonChest,UncommonChest,
        RareChest,Grenade,Knife,Pistol,Rifle,Shotgun,Sniper,Helmet,Pumpkin,Glasses,
        GrenadePoint,KnifePoint,PistolPoint,SMGPoint,RiflePoint,ShotgunPoint,SniperPoint,ArmorPoint}
    public enum RewardRarity { Common = 0, Uncommon = 1, Rare = 2 }
}
