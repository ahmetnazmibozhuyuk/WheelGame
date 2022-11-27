using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wheel.Reward;

namespace Wheel.Control
{
    public class RewardTester : MonoBehaviour
    {
        public bool ShouldOverrideRewards = false;


        public List<RewardAttributes> Rewards = new List<RewardAttributes>();
    }
}
