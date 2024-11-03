using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    [CreateAssetMenu(fileName = "ResearchSpeedBonus", menuName = "PlayerBonus/ResearchSpeedBonus", order = 0)]
    public class ResearchSpeedBonus : PlayerBonus
    {
        [field: SerializeField]
        public float ResearchSpeedMultiplier { get; private set; }

        public override void Apply(IObjectResolver objectResolver)
        {
            var researchManager = objectResolver.Resolve<ResearchManager>();
            researchManager.UpgradeResearchSpeed(researchManager.ResearchSpeedMultiplier * ResearchSpeedMultiplier);
        }
    }
}
