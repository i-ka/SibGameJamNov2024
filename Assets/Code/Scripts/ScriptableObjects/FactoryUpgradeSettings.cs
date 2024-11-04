using System;
using System.Collections.Generic;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using SibGameJam.TankFactorySpace;
using UnityEngine;

namespace SibGameJam.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FactoryUpgradeSettings", menuName = "FactoryUpgradeSettings", order = 0)]
    public class FactoryUpgradeSettings : ScriptableObject
    {
        [field: SerializeField]
        public FactoryLevelSettigs[] Levels { get; private set; }
    }

    [Serializable]
    public class FactoryLevelSettigs
    {
        public int PointCost;
        public PlayerBonus[] UpgradeVariants;
    }

}
