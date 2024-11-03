using Code.Scripts.Ui;
using FS.Gameplay.PlayerVehicle;
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
        [SerializeField]
        private PlayerLevelingConfiguration _levelingConfiguration;
        [SerializeField]
        private FactoryUpgradeSettings _factoryUpgradeSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelingConfiguration);
            builder.RegisterInstance(_factoryUpgradeSettings);

            builder.Register<ResearchManager>(Lifetime.Singleton);
            builder.Register<TankManager>(Lifetime.Singleton);

            RegisterComponentFromScene<LevelingUi>(builder);
            builder.RegisterEntryPoint<LevelingUiController>();

            builder.Register<FactoryUpgradeManager>(Lifetime.Singleton);

            RegisterComponentFromScene<VehicleController>(builder);

            RegisterComponentFromScene<FactoryUpgradeUi>(builder);
            builder.RegisterEntryPoint<FactoryUpgradeUiController>();

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
