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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelingConfiguration);
            builder.Register<ResearchManager>(Lifetime.Singleton);
            builder.Register<TankManager>(Lifetime.Singleton);
        }
    }
}
