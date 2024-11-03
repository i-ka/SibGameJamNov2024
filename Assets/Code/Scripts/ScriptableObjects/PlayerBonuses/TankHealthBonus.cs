using System.Linq;
using FS.Gameplay.PlayerVehicle;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "TanksHealthBonus", menuName = "FactoryBonus/TanksHealthBonus", order = 0)]
    public class TanksHealthBonus : PlayerBonus
    {
        [field: SerializeField]
        public float HealthMultiplier { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        { 
            Debug.Log($"TanksHealthBonus Apply with {HealthMultiplier} health");
        }
    }
}