using System.Linq;
using FS.Gameplay.PlayerVehicle;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "TanksSpeedBonus", menuName = "FactoryBonus/TanksSpeedBonus", order = 0)]
    public class TanksSpeedBonus : PlayerBonus
    {
        [field: SerializeField]
        public int Speed { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        { 
            Debug.Log($"TanksSpeedBonus apply with {Speed} kmph");
        }
    }
}