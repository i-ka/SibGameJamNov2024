using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.HUD
{
    public class PlayerHUD : MonoBehaviour
    {
        [Space]
        [Header("Tank Stats")]
        [SerializeField] private TextMeshProUGUI textSpeedValue;
        [Space]
        [Header("Tank Health")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI textHealthValue;
        [SerializeField] private Image healthBar;
        [Space]
        [Header("Tank Health Amount")]
        [SerializeField] private float verticalDistance;
        [SerializeField] private float effectDuration;
        [SerializeField] private Canvas healthStatsCanvas;
        [SerializeField] private GameObject damageIndicatorPrefab;
        [SerializeField] private GameObject healIndicatorPrefab;

        // private 
        Vector3 tankPosition;

        public void Init()
        {
        }

        public void SetTankPosition(Vector3 position)
        {
            tankPosition = position;
        }

        public void SetTankSpeed(float speed)
        {

            textSpeedValue.text = $"{Mathf.RoundToInt(speed)}";
        }

        public void SetHealth(int lastDamage, int currentHealth, int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            // textHealthValue.text = $"{currentHealth} / {maxHealth}";
            // healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        }

        public void DeactivateHud()
        {
            gameObject.SetActive(false);
        }

        public IEnumerator ShowTankDamage(int lastDamage, Vector3 tankPosition)
        {
            float elapsed = effectDuration;

            GameObject newDamageObject = Instantiate(damageIndicatorPrefab, Camera.main.WorldToScreenPoint(tankPosition), Quaternion.identity, healthStatsCanvas.transform);
            TextMeshProUGUI damageText = newDamageObject.GetComponentInChildren<TextMeshProUGUI>();
            while (elapsed > 0)
            {
                Vector3 moveUp = Vector3.LerpUnclamped(new Vector3(0, verticalDistance, 0), Vector3.zero, elapsed);
                newDamageObject.transform.position = Camera.main.WorldToScreenPoint(tankPosition + new Vector3(0, 15, 0) + moveUp);

                damageText.alpha = (elapsed / effectDuration) * 1.0f;
                damageText.text = $"-{lastDamage}";

                elapsed -= Time.deltaTime;

                yield return null;
            }

            Destroy(newDamageObject);
        }

        public IEnumerator ShowTankHeal(float heal, Vector3 tankPosition)
        {
            float elapsed = effectDuration;

            GameObject newDamageObject = Instantiate(healIndicatorPrefab, Camera.main.WorldToScreenPoint(tankPosition), Quaternion.identity, healthStatsCanvas.transform);
            TextMeshProUGUI damageText = newDamageObject.GetComponentInChildren<TextMeshProUGUI>();

            while (elapsed > 0)
            {
                Vector3 moveUp = Vector3.LerpUnclamped(new Vector3(0, verticalDistance, 0), Vector3.zero, elapsed);
                newDamageObject.transform.position = Camera.main.WorldToScreenPoint(tankPosition + new Vector3(0, 15, 0) + moveUp);

                damageText.alpha = (elapsed / effectDuration) * 1.0f;
                damageText.text = $"+{heal}";

                elapsed -= Time.deltaTime;

                yield return null;
            }

            Destroy(newDamageObject);
        }
    }
}


