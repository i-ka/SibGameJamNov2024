using System;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.Ui
{
    public class FactoryUpgradeButton : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TextMeshProUGUI UpgradeNameText { get; private set; }

        public event Action<PlayerBonus> BonusClicked;

        private PlayerBonus _bonus;

        public void Init(PlayerBonus bonus)
        {
            UpgradeNameText.text = bonus.BonusName;
            _bonus = bonus;
            Button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            BonusClicked?.Invoke(_bonus);
        }
    }
}