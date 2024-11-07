using System.Linq;
using Code.Scripts.AI.Data;
using Code.Scripts.GameServices;
using Code.Gameplay.PlayerVehicle;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "TanksHealthBonus", menuName = "FactoryBonus/TanksHealthBonus", order = 0)]
    public class TanksHealthBonus : PlayerBonus
    {
        [field: SerializeField]
        public float HealthMultiplier { get; private set; }
        [field: SerializeField]
        public Team Team { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        {
            var registeredFabric = objectResolver.Resolve<FactoryRegistry>();
            var factory = registeredFabric.GetFabric(Team);
            // TODO 
            factory.UpgradeHealth(HealthMultiplier);
        }
    }
}