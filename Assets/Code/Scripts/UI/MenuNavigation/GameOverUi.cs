using Code.Scripts.GameServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI.MenuNavigation
{
    public class GameOverUi: MonoBehaviour
    {
        [field:SerializeField]
        public TextMeshProUGUI Text { get; private set; }
        
        [field:SerializeField]
        public Button RestartButton { get; private set; }
        
        [field:SerializeField]
        public Button ToMainMenuButton { get; private set; }
        
        public void SetReason(GameOverReason reason)
        {
            Text.text = GetText(reason);
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        private string GetText(GameOverReason reason) => reason switch
        {
            GameOverReason.Win => "Ваша команда победила в битве",
            GameOverReason.LooseBaseDestroyed => "Ваша команда проиграла в битве",
            GameOverReason.LoosePlayerDied => "Робот-ремонтник был уничтожен",
            _ => "Случлось что-то непонятное"
        };
    }
}