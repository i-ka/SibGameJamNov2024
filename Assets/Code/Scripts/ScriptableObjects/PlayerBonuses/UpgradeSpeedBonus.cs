using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "UpgradeSpeedBonus", menuName = "PlayerBonus/UpgradeSpeedBonus", order = 1)]
    public class UpgradeSpeedBonus : PlayerBonus
    {
        [field: SerializeField]
        public float BonusSpeedMultiplier { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        {
            Debug.Log($"Add {BonusSpeedMultiplier} to player veihcle");
        }
    }
}
