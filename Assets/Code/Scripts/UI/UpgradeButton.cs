using System;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Ui
{
    public class UpgradeButton : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TextMeshProUGUI UpgradeNameText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI UpgradeDescriptionText { get; private set; }

        public event Action<PlayerBonus> BonusClicked;

        private PlayerBonus _bonus;

        public void Init(PlayerBonus bonus)
        {
            UpgradeNameText.text = bonus.BonusName;
            UpgradeDescriptionText.text = bonus.Description;
            _bonus = bonus;
            Button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            BonusClicked?.Invoke(_bonus);
        }
    }
}