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
		[SerializeField] private float _lifeTimeSeconds;

		private void OnEnable()
		{
			StartCoroutine(DestroyWithDelay());
		}

		private IEnumerator DestroyWithDelay()
		{
			yield return new WaitForSeconds(_lifeTimeSeconds);
			gameObject.SetActive(false);
		}

		public void SetDamage(int damage)
		{
			_damage = damage;
		}

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

			HealthController healthObject = collision.gameObject.GetComponentInParent<HealthController>();

			if (healthObject)
			{
				healthObject.ReduceHealth(_damage);
			}

			StartCoroutine(DisableWithDelay());
		}

		private IEnumerator DisableWithDelay()
		{
			yield return new WaitForSeconds(_disablingDelaySeconds);
			_damage = 0;
			gameObject.SetActive(false);
			_isDisabling = false;
		}
	}
}