using System;
using Code.Scripts.AI.Data;
using VContainer.Unity;

namespace Code.Scripts.GameServices
{
    public class GameFlowService : IInitializable
    {
        public event Action OnWin;
        public event Action OnLose; 
        
        private readonly FactoryRegistry factoryRegistry;
        
        public GameFlowService(FactoryRegistry factoryRegistry)
        {
            this.factoryRegistry = factoryRegistry;
        }
        
        public void Initialize()
        {
            factoryRegistry.GetFabric(Team.Red).HealthController.OnDestroyed += () =>  OnLose?.Invoke();
            factoryRegistry.GetFabric(Team.Blue).HealthController.OnDestroyed += () => OnWin?.Invoke();
        }
    }
}