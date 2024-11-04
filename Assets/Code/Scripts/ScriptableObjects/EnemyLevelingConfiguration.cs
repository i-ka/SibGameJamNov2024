using System;
using System.Collections.Generic;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using UnityEngine;

namespace SibGameJam.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyLevelingConfiguration", menuName = "EnemyLevelingConfiguration", order = 0)]
    public class EnemyLevelingConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public List<LevelData> Levels { get; private set; }
    }
}