using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
	public class TankSkeleton : MonoBehaviour
	{
		[SerializeField] private float _lifeTimeSeconds;

		private void Awake()
		{
			StartCoroutine(DestroyWithDelay());
		}

		private IEnumerator DestroyWithDelay()
		{
			yield return new WaitForSeconds(_lifeTimeSeconds);
			Destroy(gameObject);
		}
	}
}