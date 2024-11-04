using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
	public class TankSkeleton : MonoBehaviour
	{
		[SerializeField] private float _lifeTimeSeconds;
		[SerializeField] private AudioSource _destroySound;

		private void Awake()
		{
			StartCoroutine(DestroyWithDelay());
			_destroySound.PlayOneShot(_destroySound.clip);
		}

		private IEnumerator DestroyWithDelay()
		{
			yield return new WaitForSeconds(_lifeTimeSeconds);
			Destroy(gameObject);
		}
	}
}