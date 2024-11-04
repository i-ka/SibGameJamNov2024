using System.Collections;
using System.Collections.Generic;
using SibGameJam.TankFactory;
using UnityEngine;

namespace Code.Scripts.TankFactory
{
	public class Resource : MonoBehaviour
	{
		[field: SerializeField] public ResourceType Type { get; set; }
		[SerializeField] private List<EventTriggerUniversal> _eventTriggers;
		[SerializeField] private float _lifeTimeSeconds;
		[SerializeField] private float _effectDurationSeconds;

		[field: SerializeField] public int Count { get; private set; }

		private bool _isDestroying;
		private Collider _collider;

		private void Awake()
		{
			_collider = GetComponent<Collider>();
			StartCoroutine(DestroyWithDelay());
		}

		private IEnumerator DestroyWithDelay()
		{
			yield return new WaitForSeconds(_lifeTimeSeconds);
			Destroy(gameObject);
		}

		/// <summary>
		///     Method to run some effects on resource and schedule object destroy
		/// </summary>
		public void Collect()
		{
			if (_isDestroying)
			{
				return;
			}
			_collider.enabled = false;
			_isDestroying = true;
			StartCoroutine(PlayEffectWithDelay());
		}

		private IEnumerator PlayEffectWithDelay()
		{
			foreach (var eventTrigger in _eventTriggers)
			{
				eventTrigger.PlayEffect();
			}

			yield return new WaitForSeconds(1f);
			Destroy(gameObject);
		}
	}
}