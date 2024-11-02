using System;
using Code.Scripts.AI.Data;

namespace SibGameJam
{
    public interface ITank
    {
        event Action<ITank> OnDestroyed;
        Team Team { get; }
    }
}