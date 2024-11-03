using UnityEngine;

namespace Code.Scripts.AI.Brain
{
	public class StateMachine
	{
		private State _currentState;
		public State CurrentState => _currentState;

		public void SetState(State newState)
		{
			_currentState?.Exit();
			_currentState = newState;
			_currentState.Enter();
		}

		public void Update()
		{
			_currentState?.Execute();
			
		}
	}
}