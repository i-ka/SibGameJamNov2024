using System.Collections.Generic;
using SibGameJam;
using SibGameJam.GameServices;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using SibGameJam.Ui;
using VContainer;
using VContainer.Unity;

namespace Code.Scripts.Ui
{
    public enum BonusType
    {
        Factory,
        Player
    }
    
    public class BonusSelection
    {
        public int Level { get; set; }
        public PlayerBonus[] Bonuses { get; set; }
        public BonusType Type { get; set; }
    }
    
    public class UpgradeUiController : IInitializable
    {
        private readonly UpgradeUi ui;
        private readonly FactoryUpgradeManager factoryUpgradeManager;
        private readonly ResearchManager researchManager;
        private readonly IObjectResolver objectResolver;

        private bool _isSelectingBonus = false;
        private Queue<BonusSelection> pendingSelections = new Queue<BonusSelection>();

        public UpgradeUiController(UpgradeUi ui, 
            FactoryUpgradeManager factoryUpgradeManager,
            ResearchManager researchManager,
            IObjectResolver objectResolver)
        {
            this.ui = ui;
            this.factoryUpgradeManager = factoryUpgradeManager;
            this.researchManager = researchManager;
            this.objectResolver = objectResolver;
        }

        public void Initialize()
        {
            factoryUpgradeManager.OnLevelUp += EnqueueFactoryBonusSelection;
            researchManager.OnLevelUp += EnqueuePlayerBonusSelection;
            ui.RootObject.SetActive(false);
        }

        private void EnqueueFactoryBonusSelection(int level, PlayerBonus[] bonuses)
        {
            pendingSelections.Enqueue(new BonusSelection
            {
                Level = level,
                Bonuses = bonuses,
                Type = BonusType.Factory
            });
            TryBeginPendingBonusSelection();
        }

        private void EnqueuePlayerBonusSelection(int level, PlayerBonus[] bonuses)
        {
            pendingSelections.Enqueue(new BonusSelection
            {
                Level = level,
                Bonuses = bonuses,
                Type = BonusType.Player
            });
            TryBeginPendingBonusSelection();
        }

        private void TryBeginPendingBonusSelection()
        {
            if (_isSelectingBonus) return;
            if (pendingSelections.Count == 0) return;
            
            BeginBonusSelect(pendingSelections.Dequeue());
        }

        private void BeginBonusSelect(BonusSelection bonusSelection)
        {
            _isSelectingBonus = true;
            ui.RootObject.SetActive(true);
            ui.InitializeBonusSelection(bonusSelection);
            ui.OnBonusSelected += OnBonusSelected;
        }

        private void OnBonusSelected(PlayerBonus bonus)
        {
            bonus.Apply(objectResolver);
            _isSelectingBonus = false;
            ui.RootObject.SetActive(false);
            ui.OnBonusSelected -= OnBonusSelected;
            
            TryBeginPendingBonusSelection();
        }

    }
}