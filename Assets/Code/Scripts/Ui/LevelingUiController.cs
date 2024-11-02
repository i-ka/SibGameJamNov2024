using VContainer.Unity;

namespace SibGameJam.Ui
{
    public class LevelingUiController: IInitializable
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
            UpdateLevel(researchManager.CurrentLevel);
            researchManager.OnPointsAdded += UpdateCurrenLevelSlider;
            UpdateCurrenLevelSlider(researchManager.PointsInCurrentLevel, researchManager.NextLevelLevelPoints);

        }

        private void UpdateLevel(int level) => ui.LevelText.text = $"{level}";

        private void UpdateCurrenLevelSlider(int currentPoints, int totalPoints)
        {
            ui.CurrentLevelProgres.maxValue = totalPoints;
            ui.CurrentLevelProgres.value = currentPoints;
        }

    }
}

