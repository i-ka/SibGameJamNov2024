using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.AI.Brain;
using Code.Scripts.AI.Data;
using SibGameJam;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Scripts.TankFactory
{
	public class TankFactory : MonoBehaviour
	{
		[SerializeField] private List<Tank> _producedTankPrefabs = new();
		[SerializeField] private List<Transform> _spawnPoints = new();
		[SerializeField] private float _productionTime = 5;
		[SerializeField] private float _maximumTanksCount;

		[SerializeField] private Team _team;

		[SerializeField] private Transform _enemyBase;

		[SerializeField] private List<Transform> _escapePoints;

		[SerializeField] private Transform _bulletPoolContainer;

		private int _currentProducedTankIndex;
		private int _currentSpawnPointIndex;

		private IObjectResolver _objectResolver;
		private TankManager _tankManager;

		[Inject]
		public void Construct(IObjectResolver objectResolver, TankManager tankManager)
		{
			_objectResolver = objectResolver;
			_tankManager = tankManager;
			StartCoroutine(SpawnTanks());
		}

		private IEnumerator SpawnTanks()
		{
			var waitForSeconds = new WaitForSeconds(_productionTime);

			while (true)
			{
				if (_tankManager.tanks.Count(tank => tank.Team == _team) < _maximumTanksCount)
				{
					SpawnTank();
				}

				yield return waitForSeconds;
			}
		}

		private void SpawnTank()
		{
			Debug.Log($"Spawn tank prefab {_currentProducedTankIndex} on spawnpoint index {_currentSpawnPointIndex}");
			var tankToInstantiate = _producedTankPrefabs[_currentProducedTankIndex];
			var spawnPoint = _spawnPoints[_currentSpawnPointIndex];

			var spawnedTank = _objectResolver.Instantiate(tankToInstantiate, spawnPoint.position, Quaternion.identity);
			spawnedTank.Initialize(_team, _enemyBase, _bulletPoolContainer, _escapePoints);
			_tankManager.RegisterTank(spawnedTank);

			_currentProducedTankIndex = (_currentProducedTankIndex + 1) % _producedTankPrefabs.Count;
			_currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % _spawnPoints.Count;
		}
	}
}