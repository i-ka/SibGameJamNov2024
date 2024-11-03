using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SibGameJam.HUD;

namespace Code.Scripts.HealthSystem
{
    public class HealthController : MonoBehaviour
    {
        #region Properties

        public int MaxHeatlth => _maxHealth;

        #endregion

        #region Variables

        [SerializeField] private int _maxHealth;

        // private
        private int _currentHealth;
        private bool _isDead = false;
        private int _lastDamageValue;
        private int _lastRepairValue;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            Debug.Log("Hello");
            OnObjectRepaired.Invoke(0, _currentHealth, _maxHealth);
        }

        public void Init()
        {
            _currentHealth = _maxHealth;
            Debug.Log("Hello");
            OnObjectRepaired.Invoke(0, _currentHealth, _maxHealth);
        }

        #endregion

        #region Events

        [SerializeField] private UnityEvent<int, int, int> OnObjectRepaired;
        [SerializeField] private UnityEvent<int, int, int> OnObjectDamaged;
        [SerializeField] private UnityEvent OnObjectDestroyed;

        #endregion

        #region Methods



        public void AddHealth(int value)
        {

            // TODO
            // calculate value btw last and current value for correct values

            _lastRepairValue = value;
            _currentHealth = Mathf.Clamp(_currentHealth + _lastRepairValue, 0, _maxHealth);
            OnObjectRepaired.Invoke(_lastRepairValue, _currentHealth, _maxHealth);
        }

        public void ReduceHealth(int value)
        {
            if (_isDead)
            {
                return;
            }

            // TODO
            // calculate value btw last and current value for correct values

            _lastDamageValue = value;
            _currentHealth = Mathf.Clamp(_currentHealth - _lastDamageValue, 0, _maxHealth);
            OnObjectDamaged.Invoke(_lastDamageValue, _currentHealth, _maxHealth);

            if(_currentHealth <= 0)
            {
                OnObjectDestroyed.Invoke();
            }

        }

        public void UpgradeMaxHealth(int healthToAdd)
        {
            _maxHealth += healthToAdd;
            _currentHealth = _maxHealth;
            OnObjectRepaired.Invoke(0, _currentHealth, _maxHealth);
        }

        #endregion
    }
}

