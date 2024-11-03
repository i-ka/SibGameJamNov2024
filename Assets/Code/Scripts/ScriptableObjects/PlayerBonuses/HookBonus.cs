using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "HookBonus", menuName = "PlayerBonus/HookBonus", order = 0)]
    public class HookBonus : PlayerBonus
    {
        public override void Apply(IObjectResolver objectResolver)
        {
            Debug.Log("Adding hook to player");
        }
    }
}
