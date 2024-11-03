using System;
using UnityEngine;

namespace FS.Gameplay.PlayerVehicle
{
    public class PlayerAbilityController: MonoBehaviour
    {
        [SerializeField] private IPlayerAbility[] abilities = new IPlayerAbility[3];

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