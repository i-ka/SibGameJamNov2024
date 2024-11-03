using System.Linq;
using FS.Gameplay.PlayerVehicle;
using UnityEngine;
using VContainer;
namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "RepairAbilityChargesBonus", menuName = "PlayerBonus/RepairAbilityChargesBonus", order = 0)]
    public class RepairAbilityChargesBonus : PlayerBonus
    {
        [field: SerializeField]
        public int BonusAbilityCharges { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        {
            var player = objectResolver.Resolve<VehicleController>();
            var repairAbility = player.AbilityController.Abilities.FirstOrDefault(a => a is RepairAbility);
            if (repairAbility == null)
                Debug.LogError("Repair Ability could not be found");
            
            (repairAbility as RepairAbility)?.AddCharges(BonusAbilityCharges);
        }
    }
}