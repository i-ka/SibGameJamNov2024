using System;
using System.Collections.Generic;
using UnityEngine;

namespace FS.Gameplay.PlayerVehicle
{
    public class PlayerAbilityController: MonoBehaviour
    {
        [SerializeField] private PlayerAbility[] abilities = new PlayerAbility[3];
        public IReadOnlyCollection<PlayerAbility> Abilities => abilities;

        [SerializeField] private KeyCode[] abilityKeys = new KeyCode[]
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
        };

        private void FixedUpdate()
        {
            for (var i = 0; i < abilities.Length; i++)
            {
                var ability = abilities[i];
                if (ability == null) return;
                if (ability.CanUse())
                {
                    if (Input.GetKey(abilityKeys[i]))
                    {
                        ability.Use();
                    }
                }
            }
        }
    }
}