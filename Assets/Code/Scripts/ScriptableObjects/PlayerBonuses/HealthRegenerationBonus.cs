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
            Debug.Log($"Add regeneration to player {HealthRegeneration} hp/sec");
        }
    }
}
