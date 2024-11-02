using System;

namespace SibGameJam
{
    public interface ITank
    {
        event Action<ITank> OnDestroyed;
        Side Side { get; }
    }
}