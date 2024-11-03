using System;
using UnityEngine;
using VContainer;

namespace SibGameJam.ScriptableObjects.PlayerBonuses
{
    public abstract class PlayerBonus : ScriptableObject
    {
        public abstract void Apply(IObjectResolver objectResolver);
    }


}
