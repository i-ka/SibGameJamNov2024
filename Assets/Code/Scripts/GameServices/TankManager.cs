using System.Collections.Generic;
using Code.Scripts.AI.Data;

namespace SibGameJam
{
    public class TankManager
    {
        private readonly ResearchManager researchManager;
        public HashSet<ITank> tanks = new HashSet<ITank>();

        public TankManager(ResearchManager researchManager)
        {
            this.researchManager = researchManager;
        }

        public void RegisterTank(ITank tank)
        {
            tanks.Add(tank);
            tank.OnDestroyed += OnTankDestroyed;
        }

        public void OnTankDestroyed(ITank tank)
        {
            if (tank.Team == Team.Blue)
                researchManager.AddResearchPoints(1);
            tanks.Remove(tank);
            tank.OnDestroyed -= OnTankDestroyed;
        }
    }
}