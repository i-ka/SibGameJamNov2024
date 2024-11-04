using Code.Scripts.AI.Data;
using System.Collections.Generic;

namespace Code.Scripts.GameServices
{
	public class FactoryRegistry
	{
		public readonly Dictionary<Team, TankFactory.TankFactory> factories = new();

		public void RegisterFabric(Team team, TankFactory.TankFactory factory)
		{
			factories.Add(team, factory);
		}

		public TankFactory.TankFactory GetFabric(Team team)
		{
			return factories[team];
		}
	}
}