using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SibGameJam.Health
{
    public class HealthController : MonoBehaviour
    {
        #region Components

        #endregion

        #region Variables
        // serializable
        [Header("Health paramaters")]
        [SerializeField] private int maxHealth;

        // private
        private float currentHealth;
        private bool isDead = false;
        private int lastDamageValue;
        private int lastRepairValue;

        #endregion

        #region Events

        [SerializeField] private UnityEvent<int> OnObjectRepaired;
        [SerializeField] private UnityEvent<int> OnObjectDamaged;
        [SerializeField] private UnityEvent OnObjectDestroyed;

        #endregion

        #region Properties

        private float GetLastDamage()
        {
            return lastDamageValue;
        }

        #endregion

        #region Init

        public void Init()
        {
            currentHealth = maxHealth;
        }

        #endregion

        #region Methods

        public void AddHealth(int value)
        {
            if (isDead) return;

            // TODO
            // calculate value btw last and current value for correct values

            lastRepairValue = value;
            currentHealth = Mathf.Clamp(currentHealth + lastRepairValue, 0, maxHealth);
            OnObjectRepaired.Invoke(lastRepairValue);

        }

        public void ReduceHealth(int value)
        {
            if (isDead) return;

            // TODO
            // calculate value btw last and current value for correct values

            lastDamageValue = value;
            currentHealth = Mathf.Clamp(currentHealth - lastDamageValue, 0, maxHealth);
            OnObjectDamaged.Invoke(lastDamageValue);

            if (currentHealth <= 0)
            {
                isDead = true;
                OnObjectDestroyed.Invoke();
            }
        }

        #endregion
    }
}

