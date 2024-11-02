using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.AI.Movement
{
	public class MovementController : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _agent;
		private Vector3 _targetPosition;

		public void SetTargetPoint(Vector3 targetPoint)
		{
			_targetPosition = targetPoint;
		}

		public void MoveToTargetPosition(Vector3 targetPosition)
		{
			_agent.SetDestination(targetPosition);
		}

		public void StopMovement()
		{
			_agent.ResetPath();
		}

		public bool HasReachedDestination()
		{
			return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
		}
	}
}