using SibGameJam.ScriptableObjects.PlayerBonuses;
using VContainer.Unity;

namespace SibGameJam.Ui
{
    public class LevelingUiController : IInitializable
    {
        private readonly LevelingUi ui;
        private readonly ResearchManager researchManager;

        public LevelingUiController(LevelingUi ui, ResearchManager researchManager)
        {
            this.ui = ui;
            this.researchManager = researchManager;
        }

        public void Initialize()
        {
            researchManager.OnLevelUp += UpdateLevel;
            UpdateLevel(researchManager.CurrentLevel, null);
            researchManager.OnPointsAdded += UpdateCurrenLevelSlider;
            UpdateCurrenLevelSlider(researchManager.PointsInCurrentLevel, researchManager.NextLevelLevelPoints);

        }

        private void UpdateLevel(int level, PlayerBonus playerBonus) => ui.LevelText.text = $"{level}";

        private void UpdateCurrenLevelSlider(float currentPoints, float totalPoints)
        {
            ui.CurrentLevelProgres.maxValue = totalPoints;
            ui.CurrentLevelProgres.value = currentPoints;
        }

    }
}


