using System.Collections.Generic;

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
            if (tank.Side == Side.Enemy)
                researchManager.AddResearchPoints(1);
            tanks.Remove(tank);
            tank.OnDestroyed -= OnTankDestroyed;
        }
    }
}