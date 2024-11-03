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

        // private
        private bool isShown = true;
        EnemyHudViewController hudViewController;

        private void Awake()
        {

            hudViewController = FindAnyObjectByType<EnemyHudViewController>();

            if (hudViewController)
            {
                hudViewController.AddToList(this);
            }
        }

        public Vector3 TankPostion()
        {
            return GetComponentInParent<Rigidbody>().position;
        }

        private void LateUpdate()
        {
            healthPanel.transform.LookAt(Camera.main.transform.position);

        }

        public void SetHealth(int lastDamage, int currentHealth, int maxHealth)
        {
            Debug.LogAssertion("Damaged");
            textHealthValue.text = $"{currentHealth}";
            fillHealthValue.fillAmount = (float)currentHealth / (float)maxHealth;
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
            if (hudViewController)
            {
                hudViewController.RemoveFromList(this);
            }
        }

        public void Show()
        {
            if (isShown) return;
            isShown = true;


            StopAllCoroutines();
            StartCoroutine(ShowProcess());
        }

        public void Hide()
        {
            if (!isShown) return;
            isShown = false;


            StopAllCoroutines();
            StartCoroutine(HideProcess());
        }

        private IEnumerator ShowProcess()
        {
            healthPanel.SetActive(true);
            transform.localScale = Vector3.zero;
            for (float t = 0; t < 1; t += Time.deltaTime * 4f)
            {
                transform.localScale = Vector3.one * t;
                yield return null;
            }

            transform.localScale = Vector3.one;
        }

        private IEnumerator HideProcess()
        {
            for (float t = 0; t < 1; t += Time.deltaTime * 4f)
            {
                transform.localScale = Vector3.one * (1f - t);
                yield return null;
            }

            healthPanel.SetActive(false);
        }
    }
}
