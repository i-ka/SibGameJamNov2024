using System;
using Code.Scripts.AI.Data;
using VContainer.Unity;

namespace Code.Scripts.GameServices
{
	public class GameFlowService : IInitializable
	{
		public event Action OnWin;
		public event Action OnLose;

		private readonly TowerRegistry towerRegistry;

		public GameFlowService(TowerRegistry towerRegistry)
		{
			this.towerRegistry = towerRegistry;
		}

		public void Initialize()
		{
			towerRegistry.GetTower(Team.Red).HealthController.OnDestroyed += () => OnLose?.Invoke();
			towerRegistry.GetTower(Team.Blue).HealthController.OnDestroyed += () => OnWin?.Invoke();
		}
	}
}