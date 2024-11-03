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
				return;
			}

			if (tank.IsAimed(tank.EnemyPosition))
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Shooting));
				return;
			}

			tank.RotateTurret(tank.EnemyPosition);
		}

		public override void Exit()
		{
		}
	}
}