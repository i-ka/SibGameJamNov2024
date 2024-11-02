using SibGameJam.GameServices;
using SibGameJam.ScriptableObjects;
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

            builder.RegisterInstance(_levelingUi);
            builder.RegisterEntryPoint<LevelingUiController>();

            builder.Register<FactoryUpgradeManager>(Lifetime.Singleton);
        }
    }
}
