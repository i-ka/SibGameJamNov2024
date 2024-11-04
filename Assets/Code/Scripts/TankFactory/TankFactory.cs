using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.AI.Brain;
using Code.Scripts.AI.Data;
using Code.Scripts.GameServices;
using Code.Scripts.HealthSystem;
using SibGameJam;
using UnityEngine;
using UnityEngine.Serialization;
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

		[field: FormerlySerializedAs("_team")]
		[field: SerializeField]
		public Team Team { get; private set; }

		[SerializeField] private Transform _enemyBase;

		[SerializeField] private List<Transform> _escapePoints;

		[SerializeField] private Transform _bulletPoolContainer;
		[SerializeField] private Color _tankColor;

		[field: SerializeField] public HealthController HealthController { get; private set; }

		[Header("Tank start parameters")] [SerializeField]
		private int _startDamage;

		[SerializeField] private int _startSpeed;
		[SerializeField] private float _startHealth;

		[SerializeField] private TankStats _tankStats;

		private int _currentProducedTankIndex;
		private int _currentSpawnPointIndex;

		private IObjectResolver _objectResolver;
		private TankManager _tankManager;


		[Inject]
		public void Construct(IObjectResolver objectResolver, TankManager tankManager, FactoryRegistry factoryRegistry)
		{
			_objectResolver = objectResolver;
			_tankManager = tankManager;

			_tankStats = new()
			{
				Health = _startHealth,
				Speed = _startSpeed,
				Damage = _startDamage,
			};

			HealthController.OnDestroyed += OnDestroyed;

			factoryRegistry.RegisterFabric(Team, this);

			StartCoroutine(SpawnTanks());
		}

		private IEnumerator SpawnTanks()
		{
			var waitForSeconds = new WaitForSeconds(_productionTime);

			while (true)
			{
				if (_tankManager.tanks.Count(tank => tank.Team == Team) < _maximumTanksCount)
				{
					SpawnTank();
				}

				yield return waitForSeconds;
			}
		}

		private void SpawnTank()
		{
			var tankToInstantiate = _producedTankPrefabs[_currentProducedTankIndex];
			var spawnPoint = _spawnPoints[_currentSpawnPointIndex];

			var spawnedTank = _objectResolver.Instantiate(tankToInstantiate, spawnPoint.position, transform.rotation);
			spawnedTank.Initialize(Team, _enemyBase, _bulletPoolContainer, _escapePoints, _tankStats, _tankColor);
			_tankManager.RegisterTank(spawnedTank);

			_currentProducedTankIndex = (_currentProducedTankIndex + 1) % _producedTankPrefabs.Count;
			_currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % _spawnPoints.Count;
		}

		public void UpgradeDamage(float damageMultiplier)
		{
			_tankStats.Damage += Mathf.RoundToInt(_tankStats.Damage * damageMultiplier);
		}

		public void UpgradeSpeed(float speedMultiplier)
		{
			_tankStats.Speed += _tankStats.Speed * speedMultiplier;
		}

		public void UpgradeHealth(float healthMultiplier)
		{
			Debug.Log("Enemy health Upgraded");
			_tankStats.Health += _tankStats.Health * healthMultiplier;
		}

		private void OnDestroyed()
		{
			Debug.Log("Tank factory destroyed");
			Destroy(gameObject);
		}
	}

	[Serializable]
	public class TankStats
	{
		private int _damage;
		private float _health;
		private float _speed;

		public int Damage
		{
			get => _damage;
			set => _damage = value;
		}

		public float Speed
		{
			get => _speed;
			set => _speed = value;
		}

		public float Health
		{
			get => _health;
			set => _health = value;
		}
	}
}