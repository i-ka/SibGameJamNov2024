using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace SibGameJam.HUD
{
    public class EnemyHUD : MonoBehaviour
    {
        [Header("Tank Health")]
        [SerializeField] private Canvas healthStatsCanvas;
        [SerializeField] private GameObject healthPanel;
        [SerializeField] private TextMeshProUGUI textHealthValue;
        [SerializeField] private Image fillHealthValue;
        [Space]
        [Header("Tank Damage")]
        [SerializeField] private float verticalDistance;
        [SerializeField] private float effectDuration;
        [SerializeField] private GameObject damageIndicatorPrefab;

        private void LateUpdate()
        {
            healthPanel.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 5, 0));
        }

        

        public void SetHealth(int lastDamage, int currentHealth, int maxHealth)
        {
            textHealthValue.text = $"{currentHealth}";
            fillHealthValue.fillAmount = currentHealth / maxHealth;
        }

        public IEnumerator ShowTankDamage(int lastDamage, Vector3 tankPosition, Camera cam)
        {
            float elapsed = effectDuration;

            GameObject newDamageObject = Instantiate(damageIndicatorPrefab, cam.WorldToScreenPoint(tankPosition), Quaternion.identity, healthStatsCanvas.transform);
            TextMeshProUGUI damageText = newDamageObject.GetComponentInChildren<TextMeshProUGUI>();

            while (elapsed > 0)
            {
                Vector3 moveUp = Vector3.LerpUnclamped(new Vector3(0, verticalDistance, 0), Vector3.zero, elapsed);
                newDamageObject.transform.position = cam.WorldToScreenPoint(tankPosition + new Vector3(0, 16, 0) + moveUp);

                damageText.alpha = (elapsed / effectDuration) * 1.0f;
                damageText.text = $"-{lastDamage}";

                elapsed -= Time.deltaTime;

                yield return null;
            }

            Destroy(newDamageObject);
        }

        public void DeactivateHud()
        {
            gameObject.SetActive(false);
        }
    }
}
