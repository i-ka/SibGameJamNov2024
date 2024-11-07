using Code.Gameplay.PlayerVehicle;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "HealthRegenerationBonus", menuName = "PlayerBonus/HealthRegenerationBonus", order = 0)]
    public class HealthRegenerationBonus : PlayerBonus
    {
        [field: SerializeField]
        public int HealthRegeneration { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        {
            var player = objectResolver.Resolve<VehicleController>();
            player.HealthController.UpgradeRegenerateValue(HealthRegeneration);
        }
    }
}
