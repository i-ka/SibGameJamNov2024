using Code.Scripts.AI.Data;
using Code.Scripts.TankFactorySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.GameServices
{
    public class FactoryRegistry
    {
        public Dictionary<Team, TankFactory> factories = new Dictionary<Team, TankFactory>();

        public void RegisterFabric(Team team, TankFactory factory)
        {
            factories.Add(team, factory);
        }

        public TankFactory GetFabric(Team team)
        {
            return factories[team];
        }
    }
}

