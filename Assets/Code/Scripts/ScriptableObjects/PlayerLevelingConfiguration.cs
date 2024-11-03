using System;
using System.Collections.Generic;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using UnityEngine;

namespace SibGameJam.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerLevelingConfiguration", menuName = "PlayerLevelingConfiguration", order = 0)]
    public class PlayerLevelingConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public List<PlayerLevelData> Levels { get; private set; }
    }

    [Serializable]
    public class PlayerLevelData
    {
        public int points;
        public PlayerBonus bonus;
    }
}