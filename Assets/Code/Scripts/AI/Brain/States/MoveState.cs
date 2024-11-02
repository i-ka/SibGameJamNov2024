namespace Code.Scripts.AI.Brain.States
{
	public class MoveState : State
	{
		public MoveState(Tank tank) : base(tank)
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
			else
			{
				tank.MoveToPosition(new(100.0F, 0.0F, 0.0F));
			}
		}

		public override void Exit()
		{
		}
	}
}