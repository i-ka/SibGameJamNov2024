using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerLevelingConfiguration", menuName = "PlayerLevelingConfiguration", order = 0)]
    public class PlayerLevelingConfiguration : ScriptableObject
    {
        [field: SerializeField]
        public List<int> Levels { get; private set; }
    }
}
