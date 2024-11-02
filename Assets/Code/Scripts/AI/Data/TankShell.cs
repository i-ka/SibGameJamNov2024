using Code.Scripts.HealthSystem;
using UnityEngine;

namespace Code.Scripts.AI.Data
{
	public class TankShell : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private float _lifeTime;
		[SerializeField] private int _damage;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private Collider _collider;

		private bool _canDamage;


		private void Update()
		{
			if (_rigidbody.velocity.magnitude > _speed)
			{
				_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _speed);
			}
		}

		private void FixedUpdate()
		{
			_rigidbody.velocity = transform.forward * _speed;
		}

		private void OnEnable()
		{
			_canDamage = true;
			gameObject.SetActive(true);
			if (_collider)
			{
				_collider.enabled = true;
			}

			Invoke(nameof(Deactivate), _lifeTime);
		}

		private void OnDisable()
		{
			if (_collider)
			{
				_collider.enabled = false;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (_canDamage == false)
			{
				return;
			}

			_canDamage = false;

			if (collision.gameObject.TryGetComponent(out HealthController healthObject))
			{
				healthObject.ReduceHealth(_damage);
			}

			Deactivate();
		}

		private void Deactivate()
		{
			CancelInvoke();
			gameObject.SetActive(false);
		}
	}
}