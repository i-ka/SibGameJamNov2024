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
			tank.RotateTurret(tank.EnemyTankPosition);
			if (tank.IsAimed(tank.EnemyTankPosition))
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