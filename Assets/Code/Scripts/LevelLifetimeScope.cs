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
        private LevelingUi _levelingUi;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelingConfiguration);
            builder.Register<ResearchManager>(Lifetime.Singleton);
            builder.Register<TankManager>(Lifetime.Singleton);

            builder.RegisterInstance(_levelingUi);
            builder.RegisterEntryPoint<LevelingUiController>();
        }
    }
}
