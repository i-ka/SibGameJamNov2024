using UnityEngine;

namespace Code.Scripts.AI.Controllers
{
	public class TurretController : MonoBehaviour
	{
		[SerializeField] private Transform _turret;
		[SerializeField] private Transform _gun;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private float _barrelMaxNegativeAngle;
		[SerializeField] private float _barrelMaxPositiveAngle;
		[SerializeField] private float _barrelElevationSpeed;

		private float _angleVerticalToTarget;
		public bool Aim(Vector3 targetPoint)
		{
			return RotateTurret(targetPoint) && RotateGun(targetPoint);
		}

		private bool RotateTurret(Vector3 targetPoint)
		{
			var direction = targetPoint - _gun.position;
			direction.y = 0;

			if (direction.magnitude == 0)
			{
				return true;
			}

			var targetRotation = Quaternion.LookRotation(direction);

			_turret.rotation = Quaternion.Slerp(_turret.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

			return Quaternion.Angle(_turret.rotation, targetRotation) < 0.1f;
		}

		private bool RotateGun(Vector3 targetPosition)
		{
			var localTargetPos = transform.InverseTransformDirection(targetPosition - _gun.position);
			var flattenedVecForBarrels = Vector3.ProjectOnPlane(localTargetPos, Vector3.up);
			var targetElevation = Vector3.Angle(flattenedVecForBarrels, localTargetPos);
			targetElevation *= Mathf.Sign(localTargetPos.y);
			targetElevation = Mathf.Clamp(targetElevation,
				-_barrelMaxNegativeAngle,
				_barrelMaxPositiveAngle);

			_angleVerticalToTarget = Mathf.MoveTowards(_angleVerticalToTarget, targetElevation, _barrelElevationSpeed * Time.fixedDeltaTime);

			if (Mathf.Abs(_angleVerticalToTarget) > 0.1f)
			{
				_gun.localEulerAngles = Vector3.right * -_angleVerticalToTarget;
			}

			var directionToTarget = (targetPosition - _gun.position).normalized;
			var angleVertical = Mathf.Abs(Vector3.Angle(_gun.forward, directionToTarget));

			return Mathf.Abs(angleVertical) < 2;
		}
	}
}