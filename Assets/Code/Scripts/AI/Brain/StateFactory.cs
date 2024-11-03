using System.Collections.Generic;
using Code.Scripts.AI.Brain.States;
using Code.Scripts.AI.Data;

namespace Code.Scripts.AI.Brain
{
	public class StateFactory
	{
		private readonly Dictionary<StateType, State> _statePool = new();

		public StateFactory(Tank tank)
		{
			_statePool.Add(StateType.Movement, new MovementState(tank));
			_statePool.Add(StateType.Aiming, new AimingState(tank));
			_statePool.Add(StateType.Shooting, new ShootingState(tank));
			_statePool.Add(StateType.Idle, new IdleState(tank));
		}

		public State GetState(StateType stateType)
		{
			return _statePool[stateType];
		}
	}
}