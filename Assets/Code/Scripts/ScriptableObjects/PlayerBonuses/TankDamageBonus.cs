using System.Linq;
using Code.Scripts.AI.Data;
using Code.Scripts.GameServices;
using FS.Gameplay.PlayerVehicle;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "TanksDamageBonus", menuName = "FactoryBonus/TanksDamageBonus", order = 0)]
    public class TankDamageBonus : PlayerBonus
    {
        [field: SerializeField]
        public float DamageMultiplier { get; private set; }
        [field: SerializeField]
        public Team Team { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        {
            var registeredFabric = objectResolver.Resolve<FactoryRegistry>();
            var factory = registeredFabric.GetFabric(Team);
            // TODO 
            factory.UpgradeDamage(DamageMultiplier);
        }
    }
}