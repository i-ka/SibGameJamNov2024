using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SibGameJam.HUD;
using Unity.VisualScripting;

namespace Code.Scripts.HealthSystem
{
    public class HealthController : MonoBehaviour
    {
        #region Properties

        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;

        #endregion

        #region Variables

        [SerializeField] private int _maxHealth;
        [SerializeField] private bool _canRegenerateHealth = true;
        [SerializeField] private int _regenerateHealthValue = 5;

        [SerializeField] private int _regenerateHealthDelay = 3;
        [SerializeField] private int _regenerateHealthOnStartDelay = 3;

        // private
        private int _currentHealth;
        private bool _isDead = false;
        private int _lastDamageValue;
        private int _lastRepairValue;
        private bool _isRegenerating = false;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            OnObjectRepaired.Invoke(0, _currentHealth, _maxHealth);
        }

        public void Init(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
            OnObjectRepaired.Invoke(0, _currentHealth, _maxHealth);
        }

        #endregion

        #region Events

        public event Action OnDestroyed;
        
        [SerializeField] private UnityEvent<int, int, int> OnObjectRepaired;
        [SerializeField] private UnityEvent<int, int, int> OnObjectDamaged;
        [SerializeField] private UnityEvent OnObjectDestroyed;

        #endregion

        #region Methods



        public void AddHealth(int value)
        {
            if (_isDead)
            {
                return;
            }

            // TODO
            // calculate value btw last and current value for correct values

            _lastRepairValue = value;
            _currentHealth = Mathf.Clamp(_currentHealth + _lastRepairValue, 0, _maxHealth);
            OnObjectRepaired.Invoke(_lastRepairValue, _currentHealth, _maxHealth);
        }

        public void RestoreHealth()
        {
            var healthToRestore = _maxHealth - _currentHealth;
            AddHealth(healthToRestore);
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
            OnObjectDamaged.Invoke(-_lastDamageValue, _currentHealth, _maxHealth);

            CancelInvoke();
            _isRegenerating = false;

            if (_canRegenerateHealth && !_isRegenerating)
            {
                InvokeRepeating(nameof(RegenerateHealth), _regenerateHealthOnStartDelay, _regenerateHealthDelay);
            }

            if (_currentHealth <= 0)
            {
                OnObjectDestroyed.Invoke();
                OnDestroyed?.Invoke();
            }

        }

        private void RegenerateHealth()
        {
            AddHealth(_regenerateHealthValue);
            _isRegenerating = true;
            if (_currentHealth >= _maxHealth)
            {
                CancelInvoke();
                _isRegenerating = false;
            }
        }

        public void UpgradeRegenerateValue(int value)
        {
            _regenerateHealthValue = value;
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

