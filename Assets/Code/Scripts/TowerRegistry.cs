using System.Collections.Generic;
using Code.Scripts.AI.Data;

namespace Code.Scripts
{
	public class TowerRegistry
	{
		private readonly Dictionary<Team, Tower> _factories = new();

		public void RegisterFabric(Team team, Tower tower)
		{
			_factories.Add(team, tower);
		}

		public Tower GetTower(Team team)
		{
			return _factories[team];
		}
	}
}