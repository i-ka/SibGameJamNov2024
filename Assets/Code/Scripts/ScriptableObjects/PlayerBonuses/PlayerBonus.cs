using System;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    public abstract class PlayerBonus : ScriptableObject
    {
        [field: SerializeField]
        public string BonusName { get; private set; }
        
        [field: SerializeField]
        public string Description { get; private set; }

        public abstract void Apply(IObjectResolver objectResolver);
    }


}
