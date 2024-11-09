using System;
using Code.Gameplay.PlayerVehicle;
using Code.Scripts.AI.Data;
using VContainer.Unity;

namespace Code.Scripts.GameServices
{
	public class GameFlowService : IInitializable
	{
		public event Action<GameOverReason> OnGameOver;

		private readonly TowerRegistry towerRegistry;
		private readonly VehicleController player;

		public GameFlowService(TowerRegistry towerRegistry, VehicleController player)
		{
			this.towerRegistry = towerRegistry;
			this.player = player;
		}

		public void Initialize()
		{
			towerRegistry.GetTower(Team.Red).HealthController.OnDestroyed += 
				() => OnGameOver?.Invoke(GameOverReason.LooseBaseDestroyed);
			
			towerRegistry.GetTower(Team.Blue).HealthController.OnDestroyed += 
				() => OnGameOver?.Invoke(GameOverReason.Win);

			player.HealthController.OnDestroyed +=
				() => OnGameOver?.Invoke(GameOverReason.LoosePlayerDied);
		}
		
		
	}
}