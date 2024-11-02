using System;
using System.Collections.Generic;
using SibGameJam.TankFactory;
using UnityEngine;

namespace SibGameJam.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FactoryUpgradeSettings", menuName = "FactoryUpgradeSettings", order = 0)]
    public class FactoryUpgradeSettings : ScriptableObject
    {
        [field: SerializeField]
        public FactoryLevelSettigs[] Levels { get; private set; }

        [field: SerializeField]
        public Dictionary<ResourceType, int> ResourcesCost { get; private set; }
    }

    [Serializable]
    public class FactoryLevelSettigs
    {
        public int PointCost;
        public FactoryUpgrade[] UpgradeVariants;
    }

    [Serializable]
    public class FactoryUpgrade
    {
        [field: SerializeField]
        public FactoryUpgradeType Type { get; private set; }

        [field: SerializeField]
        public float Value { get; private set; }
    }

    public enum FactoryUpgradeType
    {
        ArmorUpgrade,
        DamageUpgrade
    }

}
