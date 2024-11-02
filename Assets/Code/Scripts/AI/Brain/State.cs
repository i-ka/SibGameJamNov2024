namespace Code.Scripts.AI.Brain
{
	public abstract class State
	{
		protected readonly Tank tank;

		protected State(Tank tank)
		{
			this.tank = tank;
		}

		public abstract void Enter();
		public abstract void Execute();
		public abstract void Exit();
	}
}