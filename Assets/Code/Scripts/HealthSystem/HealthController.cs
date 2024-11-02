using SibGameJam.HUD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.HealthSystem
{
	public class HealthController : MonoBehaviour
	{
#region Properties

        [SerializeField] private PlayerHUD playerHUD;

        #endregion

#endregion

        // private
        private int currentHealth;
        private bool isDead = false;
        private int lastDamageValue;
        private int lastRepairValue;

		public void Init()
		{
			_currentHealth = _maxHealth;
		}

#endregion

        [SerializeField] private UnityEvent<int, int, int> OnObjectRepaired;
        [SerializeField] private UnityEvent<int, int, int> OnObjectDamaged;
        [SerializeField] private UnityEvent OnObjectDestroyed;

#endregion

#region Events

		[SerializeField] private UnityEvent<int> _onObjectRepaired;
		[SerializeField] private UnityEvent<int> _onObjectDamaged;
		[SerializeField] private UnityEvent _onObjectDestroyed;

#endregion

#region Methods

        public void Init()
        {
            currentHealth = maxHealth;
            Debug.Log("Hello");
            OnObjectRepaired.Invoke(0, currentHealth, maxHealth);
        }

			// TODO
			// calculate value btw last and current value for correct values

			_lastRepairValue = value;
			_currentHealth = Mathf.Clamp(_currentHealth + _lastRepairValue, 0, _maxHealth);
			_onObjectRepaired.Invoke(_lastRepairValue);
		}

		public void ReduceHealth(int value)
		{
			if (_isDead)
			{
				return;
			}

			// TODO
			// calculate value btw last and current value for correct values

            lastRepairValue = value;
            currentHealth = Mathf.Clamp(currentHealth + lastRepairValue, 0, maxHealth);
            OnObjectRepaired.Invoke(lastRepairValue, currentHealth, maxHealth);

        }

        public void ReduceHealth(int value)
        {
            if (isDead) return;

            // TODO
            // calculate value btw last and current value for correct values

            lastDamageValue = value;
            currentHealth = Mathf.Clamp(currentHealth - lastDamageValue, 0, maxHealth);
            OnObjectDamaged.Invoke(lastDamageValue, currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                isDead = true;
                OnObjectDestroyed.Invoke();
            }
        }

        #endregion
    }
}

