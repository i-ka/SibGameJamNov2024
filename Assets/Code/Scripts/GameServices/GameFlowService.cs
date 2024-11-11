using System;
using Code.Gameplay.PlayerVehicle;
using Code.Scripts.AI.Data;
using VContainer.Unity;

namespace Code.Scripts.GameServices
{
	public class GameFlowService : IStartable
	{
		public event Action<GameOverReason> OnGameOver;

		private readonly TowerRegistry towerRegistry;
		private readonly VehicleController player;

		public GameFlowService(TowerRegistry towerRegistry, VehicleController player)
		{
			this.towerRegistry = towerRegistry;
			this.player = player;
		}

		public void Start()
		{
			towerRegistry.GetTower(Team.Red).HealthController.OnDestroyed += 
				() => GameOver(GameOverReason.LooseBaseDestroyed);
			
			towerRegistry.GetTower(Team.Blue).HealthController.OnDestroyed += 
				() => GameOver(GameOverReason.Win);

			player.HealthController.OnDestroyed +=
				() => GameOver(GameOverReason.LoosePlayerDied);
		}

		private void GameOver(GameOverReason reason)
		{
			player.SetInputEnabled(false);
			OnGameOver?.Invoke(reason);
		}

		
	}
}