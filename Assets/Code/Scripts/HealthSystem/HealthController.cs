using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.HealthSystem
{
	public class HealthController : MonoBehaviour
	{
#region Properties

		private float GetLastDamage()
		{
			return _lastDamageValue;
		}

#endregion

#region Init

		public void Init()
		{
			_currentHealth = _maxHealth;
		}

#endregion

#region Variables
		
		[Header("Health paramaters")] [SerializeField]
		private int _maxHealth;
		
		private float _currentHealth;
		private bool _isDead;
		private int _lastDamageValue;
		private int _lastRepairValue;

#endregion

#region Events

		[SerializeField] private UnityEvent<int> _onObjectRepaired;
		[SerializeField] private UnityEvent<int> _onObjectDamaged;
		[SerializeField] private UnityEvent _onObjectDestroyed;

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

			_lastDamageValue = value;
			_currentHealth = Mathf.Clamp(_currentHealth - _lastDamageValue, 0, _maxHealth);
			_onObjectDamaged.Invoke(_lastDamageValue);

			if (_currentHealth <= 0)
			{
				_isDead = true;
				_onObjectDestroyed.Invoke();
			}
		}

#endregion
	}
}