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
			_statePool.Add(StateType.Move, new MoveState(tank));
		}

		public State GetState(StateType stateType)
		{
			return _statePool[stateType];
		}
	}
}