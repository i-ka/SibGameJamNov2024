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
			if (tank.CanSeeEnemy() && !tank.CanShotEnemy())
			{
				tank.MoveToPosition(tank.EnemyTankPosition);
			}

			if (tank.CanSeeEnemy() && tank.CanShotEnemy())
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Aiming));
				//tank.StateMachine.SetState(new AimingState(tank));
			}

			if (!tank.CanSeeEnemy() && !tank.CanShotEnemy())
			{
				if (NavMesh.SamplePosition(tank.BaseTransform.position, out var hit, 100, NavMesh.AllAreas))
				{
					tank.MoveToPosition(hit.position);
				}
				//tank.MoveToPosition(new(0.0F, 0.0F, 100.0F));
			}
		}

		public override void Exit()
		{
			tank.Stop();
		}
	}
}