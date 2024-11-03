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
            Debug.Log($"Adding {BonusAbilityCharges} to player repair ability");
        }
    }
}