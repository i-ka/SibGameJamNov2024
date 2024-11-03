using Code.Scripts.AI.Data;
using UnityEngine;

namespace Code.Scripts.AI.Brain.States
{
	public class IdleState : State
	{
		public IdleState(Tank tank) : base(tank)
		{
		}

		public override void Enter()
		{
		}

		public override void Execute()
		{
			Debug.LogError("Idle");
			if (tank.CanSeeEnemy() && tank.CurrentHealth < tank.EscapeEscapeZoneThreshold)
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Movement));
			}

			if (tank.CanSeeEnemy() && tank.CanShotEnemy() && tank.CurrentHealth > tank.EscapeEscapeZoneThreshold)
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Aiming));
			}

			if (tank.CurrentHealth > tank.HealingMax)
			{
				tank.StateMachine.SetState(tank.StateFactory.GetState(StateType.Movement));
			}

			tank.Stop();
		}

		public override void Exit()
		{
		}
	}
}