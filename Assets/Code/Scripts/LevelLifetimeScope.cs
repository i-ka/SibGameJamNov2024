using Code.Scripts;
using Code.Scripts.GameServices;
using Code.Scripts.Ui;
using Code.Gameplay.PlayerVehicle;
using SibGameJam.GameServices;
using SibGameJam.ScriptableObjects;
using SibGameJam.Ui;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SibGameJam
{
	public class LevelLifetimeScope : LifetimeScope
	{
		[SerializeField] private PlayerLevelingConfiguration _levelingConfigurationPlayer;
		[SerializeField] private FactoryUpgradeSettings _factoryUpgradeSettings;

		[SerializeField] private EnemyLevelingConfiguration _levelingConfigurationEnemy;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(_levelingConfigurationPlayer);
			builder.RegisterInstance(_factoryUpgradeSettings);

			builder.RegisterInstance(_levelingConfigurationEnemy);
			builder.RegisterEntryPoint<EnemyResearchManager>();

			builder.Register<ResearchManager>(Lifetime.Singleton);
			builder.Register<TankManager>(Lifetime.Singleton);
			builder.Register<FactoryRegistry>(Lifetime.Singleton);
			builder.Register<TowerRegistry>(Lifetime.Singleton);

			RegisterComponentFromScene<LevelingUi>(builder);
			builder.RegisterEntryPoint<LevelingUiController>();

			builder.Register<FactoryUpgradeManager>(Lifetime.Singleton);

			RegisterComponentFromScene<VehicleController>(builder);

			RegisterComponentFromScene<FactoryUpgradeUi>(builder);
			builder.RegisterEntryPoint<FactoryUpgradeUiController>();

			RegisterComponentFromScene<PlayerResourcesUi>(builder);
			builder.RegisterEntryPoint<PlayerResourceUiController>();

			builder.RegisterEntryPoint<GameFlowService>();
		}

		private void RegisterComponentFromScene<TComponentType>(IContainerBuilder builder) where TComponentType : MonoBehaviour
		{
			var component = FindAnyObjectByType<TComponentType>();
			if (component != null)
				builder.RegisterInstance(component);
			else
				Debug.LogWarning($"Player {typeof(TComponentType).Name} not found on scene");
		}
	}
}