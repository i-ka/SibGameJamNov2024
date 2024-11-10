using System;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    public abstract class PlayerBonus : ScriptableObject
    {
        [field: SerializeField]
        [field: TextArea]
        public string BonusName { get; private set; }
        
        [field: SerializeField]
        [field: TextArea]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }

        public abstract void Apply(IObjectResolver objectResolver);
    }


}
