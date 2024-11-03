using System;

namespace FS.Gameplay.PlayerVehicle
{
    public interface IPlayerAbility
    {
        bool CanUse();
        void Use();

        event Action<float> OnProgressChanged;
        event Action OnFinished;
    }
}