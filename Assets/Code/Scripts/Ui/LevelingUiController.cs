using SibGameJam.GameServices;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using VContainer.Unity;

namespace SibGameJam.Ui
{
    public class LevelingUiController : IInitializable
    {
        private readonly LevelingUi ui;
        private readonly ResearchManager researchManager;
        private readonly FactoryUpgradeManager factoryUpgradeManager;

        public LevelingUiController(LevelingUi ui, ResearchManager researchManager, FactoryUpgradeManager factoryUpgradeManager)
        {
            this.ui = ui;
            this.researchManager = researchManager;
            this.factoryUpgradeManager = factoryUpgradeManager;
        }

        public void Initialize()
        {
            researchManager.OnLevelUp += UpdateLevel;
            UpdateLevel(researchManager.CurrentLevel, null);
            researchManager.OnPointsAdded += UpdateCurrenLevelSlider;
            UpdateCurrenLevelSlider(researchManager.PointsInCurrentLevel, researchManager.NextLevelLevelPoints);

            factoryUpgradeManager.OnLevelUp += UpdateFactoryLevel;
            factoryUpgradeManager.OnPointsAdded += UpdateCurrenFactoryLevelSlider;
            UpdateFactoryLevel(factoryUpgradeManager.CurrentLevel, null);
            UpdateCurrenFactoryLevelSlider(factoryUpgradeManager.PointsInCurrentLevel, factoryUpgradeManager.NextLevelLevelPoints);
        }

        private void UpdateLevel(int level, PlayerBonus playerBonus) => ui.LevelText.text = $"{level + 1}";

        private void UpdateCurrenLevelSlider(float currentPoints, float totalPoints)
        {
            ui.CurrentLevelProgres.maxValue = totalPoints;
            ui.CurrentLevelProgres.value = currentPoints;
        }

        private void UpdateFactoryLevel(int level, PlayerBonus[] bonuses)
        {
            if (ui.FactoryLevelText)
            {
                ui.FactoryLevelText.text = $"{level + 1}";
            }
        }
        

        private void UpdateCurrenFactoryLevelSlider(int currentPoints, int totalPoints)
        {
            ui.FactoryCurrentLevelProgress.maxValue = totalPoints;
            ui.FactoryCurrentLevelProgress.value = currentPoints;
        }

    }
}


