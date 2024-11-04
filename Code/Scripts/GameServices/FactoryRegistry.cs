using Code.Scripts.AI.Data;
using Code.Scripts.TankFactory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.GameServices
{
    public class FactoryRegistry
    {
        public Dictionary<Team, TankFactory> factories = new Dictionary<Team, TankFactory>();
    }
}

