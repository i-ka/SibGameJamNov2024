using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "BoostAbilityTimeBonus", menuName = "PlayerBonus/BoostAbilityTimeBonus", order = 0)]
    public class BoostAbilityTimeBonus : PlayerBonus
    {
        [field: SerializeField]
        public float AdditionTime { get; private set; }
        public override void Apply(IObjectResolver objectResolver)
        {
            Debug.Log($"Give addition boost ability time {AdditionTime}s");
        }
    }
}

