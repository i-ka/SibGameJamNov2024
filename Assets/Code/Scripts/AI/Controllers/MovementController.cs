using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.AI.Controllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        public void Init(float speed)
        {
            if (speed != 0) _agent.speed = speed;
        }

        public void MoveToTargetPosition(Vector3 targetPosition)
        {
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