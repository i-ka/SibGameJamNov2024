using System;
using SibGameJam.ScriptableObjects;
using UnityEngine;

namespace Code.Gameplay.PlayerVehicle
{
    public abstract class PlayerAbility: MonoBehaviour
    {
        [field:SerializeField]
        public AbilityData AbilityData { get; private set; }
        
        public abstract bool CanUse();
        public abstract void Use();
        
        protected void EmitProgressChanged(float progress) => OnProgressChanged?.Invoke(progress);
        protected void EmitFinished() => OnFinished?.Invoke();

        public event Action<float> OnProgressChanged;
        public event Action OnFinished;
    }
}