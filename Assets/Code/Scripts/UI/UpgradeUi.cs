using System;
using Code.Scripts.Ui;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Ui
{
    public class UpgradeUi : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject RootObject { get; private set; }
        
        [SerializeField]
        private GameObject buttonsParent;

        [SerializeField] private UpgradeButton buttonPrefab;

        [SerializeField] private TextMeshProUGUI upgradeCaption;

        public event Action<PlayerBonus> OnBonusSelected;

        public void InitializeBonusSelection(BonusSelection bonusSelection)
        {
            CleanButtons();
            upgradeCaption.text = GetCaptionText(bonusSelection.Type);
            foreach (var bonus in bonusSelection.Bonuses)
            {
                var button = Instantiate(buttonPrefab, buttonsParent.transform);
                button.Init(bonus);
                button.BonusClicked += OnBonusButtonClicked;
            }
        }

        private string GetCaptionText(BonusType type) => type switch
        {
            BonusType.Factory => "Выберете улучшение фабрики",
            BonusType.Player => "Выберете улучшение робота",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        
        private void CleanButtons()
        {
            foreach (Transform button in buttonsParent.transform)
            {
                button.GetComponent<UpgradeButton>().BonusClicked -= OnBonusButtonClicked;
                Destroy(button.gameObject);
            }
        }

        private void OnBonusButtonClicked(PlayerBonus bonus) => OnBonusSelected?.Invoke(bonus);

    }
}
