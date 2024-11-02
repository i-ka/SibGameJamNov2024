using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.AI.Controllers
{
	public class MovementController : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _agent;

		public void MoveToTargetPosition(Vector3 targetPosition)
		{
			Debug.LogError("Moving to target position");
			_agent.isStopped = false;
			_agent.SetDestination(targetPosition);
		}

		public void StopMovement()
		{
			_agent.ResetPath();
			_agent.isStopped = true;
		}
	}
}