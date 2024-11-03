using System;
using Code.Scripts.AI.Data;
using Code.Scripts.HealthSystem;

namespace SibGameJam
{
    public interface ITank
    {
        event Action<ITank> OnDestroyed;
        Team Team { get; }
        
        HealthController HealthController { get; }
    }
}