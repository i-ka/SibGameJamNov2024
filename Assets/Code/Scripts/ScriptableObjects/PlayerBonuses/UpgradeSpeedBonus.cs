using Code.Gameplay.PlayerVehicle;
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
            var player = objectResolver.Resolve<VehicleController>();
            var speedBonus = player.MovementController.MaxForwardSpeedInKmpH * BonusSpeedMultiplier;
            player.MovementController.UpgradeMaxForwardSpeed(speedBonus);
        }
    }
}
