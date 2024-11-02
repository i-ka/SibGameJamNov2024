using UnityEngine;

namespace Code.Scripts.AI.Controllers
{
	public class TurretController : MonoBehaviour
	{
		[SerializeField] private Transform _turret;
		[SerializeField] private float _rotationSpeed;

		public bool RotateTurret(Vector3 targetPoint)
		{
			var direction = targetPoint - transform.position;
			direction.y = 0;

			if (direction.magnitude == 0)
			{
				return true;
			}

			var targetRotation = Quaternion.LookRotation(direction);

			_turret.rotation = Quaternion.Slerp(_turret.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

			return Quaternion.Angle(_turret.rotation, targetRotation) < 0.1f;
		}
	}
}