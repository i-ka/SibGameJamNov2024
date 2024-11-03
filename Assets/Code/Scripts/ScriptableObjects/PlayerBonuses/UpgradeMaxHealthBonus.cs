using FS.Gameplay.PlayerVehicle;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "UpgradeMaxHealthBonus", menuName = "PlayerBonus/UpgradeMaxHealthBonus", order = 0)]
    public class UpgradeMaxHealthBonus : PlayerBonus
    {

        [field: SerializeField]
        public float BonusHealthMultiplier { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        {
            var player = objectResolver.Resolve<VehicleController>();
            player.HealthController.UpgradeMaxHealth((int)(player.HealthController.MaxHealth * BonusHealthMultiplier));
        }
    }
}
