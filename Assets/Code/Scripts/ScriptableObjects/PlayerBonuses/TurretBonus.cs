using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "TurretBonus", menuName = "PlayerBonus/TurretBonus", order = 0)]
    public class TurretBonus : PlayerBonus
    {
        public override void Apply(IObjectResolver objectResolver)
        {
            Debug.Log("Give player a turret");
        }
    }
}

