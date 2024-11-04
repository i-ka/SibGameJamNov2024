using Code.Scripts.AI.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.AI.Brain.States
{
	public class MovementState : State
	{
		public MovementState(Tank tank) : base(tank)
		{
		}

		public override void Enter()
		{
		}

		public override void Execute()
		{
			if (tank.CurrentHealth < tank.EscapeThresholdHealth)
			{
				var distanceToNearestPoint = float.MaxValue;
				Transform nearestPoint = null;
				foreach (var point in tank.EscapePoints)
				{
					var distance = CalculatePathDistance(tank.transform.position, point.position);
					if (distance < distanceToNearestPoint)
					{
						distanceToNearestPoint = CalculatePathDistance(tank.transform.position, point.position);
						nearestPoint = point;
					}
				}

				if (nearestPoint)
				{
					if (NavMesh.SamplePosition(nearestPoint.position, out var point, 1000, NavMesh.AllAreas))
					{
						tank.MoveToPosition(point.position);
					}

					if (Vector3.Distance(tank.transform.position, nearestPoint.position) < 1.6F)
					{
						tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Idle));
					}
				}

				return;
			}

			if (tank.CanSeeEnemy() && !tank.CanShotEnemy())
			{
				tank.MoveToPosition(tank.Enemy.transform.position);
				return;
			}

			if (tank.CanSeeEnemy() && tank.CanShotEnemy())
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Aiming));
				return;
			}

			if (!tank.CanSeeEnemy() && !tank.CanShotEnemy())
			{
				if (NavMesh.SamplePosition(tank.BaseTransform.position, out var point, 1000, NavMesh.AllAreas))
				{
					tank.MoveToPosition(point.position);
				}
			}
		}

		float CalculatePathDistance(Vector3 start, Vector3 end)
		{
			var path = new NavMeshPath();
			return NavMesh.CalculatePath(start, end, NavMesh.AllAreas, path) ? GetPathLength(path) : Mathf.Infinity;
		}

		float GetPathLength(NavMeshPath path)
		{
			var length = 0.0f;

			for (var i = 1; i < path.corners.Length; i++)
			{
				length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
			}

			return length;
		}


		public override void Exit()
		{
			tank.Stop();
		}
	}
}