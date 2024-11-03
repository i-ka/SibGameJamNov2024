using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "ChainsawBonus", menuName = "PlayerBonus/ChainsawBonus", order = 0)]
    public class ChainsawBonus : PlayerBonus
    {
        public override void Apply(IObjectResolver objectResolver)
        {
            Debug.Log("Give player a chaninsaw");
        }
    }
}

