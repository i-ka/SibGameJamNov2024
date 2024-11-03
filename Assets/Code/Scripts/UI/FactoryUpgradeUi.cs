using System;
using Code.Scripts.Ui;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Ui
{
    public class FactoryUpgradeUi : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject RootObject { get; private set; }
        
        [SerializeField]
        private GameObject buttonsParent;

        [SerializeField] private FactoryUpgradeButton buttonPrefab;

        public event Action<PlayerBonus> OnBonusSelected;


        public void InitializeBonusSelection(PlayerBonus[] bonuses)
        {
            CleanButtons();
            foreach (var bonus in bonuses)
            {
                var button = Instantiate(buttonPrefab, buttonsParent.transform);
                button.Init(bonus);
                button.BonusClicked += OnBonusButtonClicked;
            }
        }
        
        private void CleanButtons()
        {
            foreach (Transform button in buttonsParent.transform)
            {
                button.GetComponent<FactoryUpgradeButton>().BonusClicked -= OnBonusButtonClicked;
                Destroy(button.gameObject);
            }
        }

        private void OnBonusButtonClicked(PlayerBonus bonus) => OnBonusSelected?.Invoke(bonus);

    }
}
