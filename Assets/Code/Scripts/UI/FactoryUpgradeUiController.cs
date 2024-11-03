using SibGameJam.GameServices;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using SibGameJam.Ui;
using VContainer;
using VContainer.Unity;

namespace Code.Scripts.Ui
{
    public class FactoryUpgradeUiController : IInitializable
    {
        private readonly FactoryUpgradeUi ui;
        private readonly FactoryUpgradeManager factoryUpgradeManager;
        private readonly IObjectResolver objectResolver;

        private bool _isSelectingBonus = false;

        public FactoryUpgradeUiController(FactoryUpgradeUi ui, 
            FactoryUpgradeManager factoryUpgradeManager, IObjectResolver objectResolver)
        {
            this.ui = ui;
            this.factoryUpgradeManager = factoryUpgradeManager;
            this.objectResolver = objectResolver;
        }

        public void Initialize()
        {
            factoryUpgradeManager.OnLevelUp += BeginBonusSelect;
            ui.RootObject.SetActive(false);
        }


        private void BeginBonusSelect(int level, PlayerBonus[] bonuses)
        {
            _isSelectingBonus = true;
            ui.RootObject.SetActive(true);
            ui.InitializeBonusSelection(bonuses);
            ui.OnBonusSelected += OnBonusSelected;
        }

        private void OnBonusSelected(PlayerBonus bonus)
        {
            bonus.Apply(objectResolver);
            _isSelectingBonus = false;
            ui.RootObject.SetActive(false);
        }

    }
}