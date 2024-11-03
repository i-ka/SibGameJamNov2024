using System.Collections;
using Code.Scripts.AI.Data;
using Code.Scripts.HealthSystem;
using UnityEngine;

namespace Code.Scripts.AI.Controllers
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _disablingDelaySeconds;
		[SerializeField] private EventTriggerForMultipleSystems _eventTriggerForMultipleSystems;
		[SerializeField] private int _damage;
		
		private bool _isDisabling;

		public Team EnemyTeam { get; set; }
		

		public void SetSpeed(float speed)
		{
			_rigidbody.velocity = transform.forward * (speed * Time.fixedDeltaTime);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (_isDisabling)
			{
				return;
			}

			_isDisabling = true;
			_eventTriggerForMultipleSystems.SendEventToVFX();
			StartCoroutine(DisableWithDelay());
			if (collision.gameObject.TryGetComponent(out HealthController healthObject))
			{
				healthObject.ReduceHealth(_damage);
			}
		}

		private IEnumerator DisableWithDelay()
		{
			yield return new WaitForSeconds(_disablingDelaySeconds);
			gameObject.SetActive(false);
			_isDisabling = false;
		}
	}
}