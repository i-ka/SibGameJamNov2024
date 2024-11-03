using Code.Scripts.AI.Data;

namespace Code.Scripts.AI.Brain.States
{
	public class ShootingState : State
	{
		public ShootingState(Tank tank) : base(tank)
		{
		}

		public override void Enter()
		{
		}

		public override void Execute()
		{
			if (tank.CanSeeEnemy() && tank.CanShotEnemy())
			{
				tank.Shoot();
			}
			else if (tank.CanSeeEnemy() && !tank.CanShotEnemy())
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Aiming));
			}
			else
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Movement));
			}
		}

		public override void Exit()
		{
		}
	}
}