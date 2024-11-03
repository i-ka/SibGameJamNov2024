using Code.Scripts.AI.Data;

namespace Code.Scripts.AI.Brain.States
{
	public class AimingState : State
	{
		public AimingState(Tank tank) : base(tank)
		{
		}

		public override void Enter()
		{
		}

		public override void Execute()
		{
			if (tank.Enemy is null)
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Movement));
			}

			tank.RotateTurret(tank.EnemyPosition);
			if (tank.IsAimed(tank.EnemyPosition))
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Shooting));
				//tank.StateMachine.SetState(new ShootingState(tank));
			}
		}

		public override void Exit()
		{
		}
	}
}