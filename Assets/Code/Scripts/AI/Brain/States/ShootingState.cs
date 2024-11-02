using UnityEngine;

namespace Code.Scripts.AI.Brain.States
{
	public class ShootingState:State
	{
		public ShootingState(Tank tank) : base(tank)
		{
		}

		public override void Enter()
		{
			Debug.LogError("Enter ShootingState");
		}

		public override void Execute()
		{
			throw new System.NotImplementedException();
		}

		public override void Exit()
		{
			Debug.LogError("Exit ShootingState");
		}
	}
}