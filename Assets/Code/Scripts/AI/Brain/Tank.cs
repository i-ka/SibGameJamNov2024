using System;
using System.Collections.Generic;
using Code.Scripts.AI.Controllers;
using Code.Scripts.AI.Data;
using Code.Scripts.HealthSystem;
using Code.Scripts.TankFactory;
using Code.Scripts.TankFactorySpace;
using FS.Gameplay.PlayerVehicle;
using SibGameJam;
using SibGameJam.TankFactorySpace;
using UnityEngine;
using MovementController = Code.Scripts.AI.Controllers.MovementController;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;


namespace Code.Scripts.AI.Brain
{
	public class Tank : MonoBehaviour, ITank
	{
		public event Action<ITank> OnDestroyed;
		[SerializeField] private GameObject _tankSkeleton;
		[SerializeField] private Resource _screw;
		[SerializeField] private int _screwsCount;
		[SerializeField] private float _screwSpawnOffset;
		[SerializeField] private Team _team;
		[SerializeField] private MovementController _movementController;
		[SerializeField] private TurretController _turretController;
		[SerializeField] private HealthController _healthController;
		[SerializeField] private Gun _gun;

		[SerializeField] private List<MeshRenderer> _detailsToPaint;

		[SerializeField] private float _shotDistance;
		[SerializeField] private int _escapeThresholdHealth;
		[SerializeField] private int _healingMax;
		[SerializeField] private int _escapeEscapeZoneThreshold;

		private Transform _bulletPoolTransform;

		public Transform BaseTransform { get; private set; }

		public Transform BulletPoolContainer
		{
			get => _bulletPoolTransform;
			set
			{
				_bulletPoolTransform = value;
				_gun.Initialize(value);
			}
		}


		public StateMachine StateMachine { get; private set; }

		public StateFactory StateFactory { get; private set; }

		public GameObject Enemy { get; private set; }

		public Vector3 EnemyPosition => Enemy.transform.position;

		public int CurrentHealth => _healthController.CurrentHealth;


		public int EscapeThresholdHealth => _escapeThresholdHealth;
		public List<Transform> EscapePoints { get; private set; }

		public int HealingMax => _healingMax;
		public int EscapeEscapeZoneThreshold => _escapeEscapeZoneThreshold;

		private void Update()
		{
			StateMachine?.Update();
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((other.TryGetComponent<Tower>(out var allyTower) && allyTower.Team == Team) || other.TryGetComponent<Tank>(out var allyTank) && allyTank.Team == Team)
			{
				return;
			}

			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team)
			{
				Enemy ??= tank.gameObject;
				return;
			}

			if (other.TryGetComponent<Tower>(out var tower) && tower.Team != Team)
			{
				Enemy ??= tower.gameObject;
				return;
			}

			if (other.TryGetComponent<HealthController>(out var healthController))
			{
				Enemy ??= healthController.gameObject;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team && other.gameObject == Enemy)
			{
				Enemy = null;
			}

			if (other.TryGetComponent<Tower>(out var tower) && other.gameObject == Enemy)
			{
				Enemy = null;
			}

			if (other.TryGetComponent<HealthController>(out var healthController) && other.gameObject == Enemy)
			{
				Enemy = null;
			}
		}

		public HealthController HealthController => _healthController;

		public Team Team
		{
			get => _team;
			private set => _team = value;
		}

		public void Initialize(Team team, Transform baseTransform, Transform bulletsPoolContainer, List<Transform> escapePoints, TankStats stats, Color color)
		{
			_healthController.Init(Mathf.RoundToInt(stats.Health));
			_movementController.Init(stats.Speed);
			_gun.SetDamage(stats.Damage);
			Team = team;
			BaseTransform = baseTransform;
			BulletPoolContainer = bulletsPoolContainer;
			EscapePoints = escapePoints;
			StateFactory = new(this);
			StateMachine ??= new();
			StateMachine.SetState(StateFactory.GetState(StateType.Movement));
			_healthController.OnDestroyed += Tank_OnDestroyed;
			Paint(color);
		}

		private void Tank_OnDestroyed()
		{
			OnDestroyed?.Invoke(this);
			Instantiate(_tankSkeleton, transform.position, transform.rotation);
			var tankPosition = transform.position;
			var screwSpawnPositions = new List<Vector3>
			{
				new(tankPosition.x - _screwSpawnOffset, tankPosition.y - 0.5F, tankPosition.z),
				new(tankPosition.x + _screwSpawnOffset, tankPosition.y - 0.5F, tankPosition.z),
				new(tankPosition.x, tankPosition.y - 0.5F, tankPosition.z + _screwSpawnOffset),
				new(tankPosition.x, tankPosition.y - 0.5F, tankPosition.z - _screwSpawnOffset),
			};
			for (var index = 0; index < _screwsCount; index++)
			{
				var screwPosition = screwSpawnPositions[Random.Range(0, screwSpawnPositions.Count)];
				var screw = Instantiate(_screw, screwPosition, Quaternion.identity);
				screw.Type = Team == Team.Red ? ResourceType.AllyWreck : ResourceType.EnemyWreck;
				screwSpawnPositions.Remove(screwPosition);
			}

			Destroy(gameObject);
		}

		public void MoveToPosition(Vector3 position)
		{
			_movementController.MoveToTargetPosition(position);
		}

		public void Stop()
		{
			_movementController.StopMovement();
		}

		public bool CanSeeEnemy()
		{
			return Enemy;
		}

		public bool CanShotEnemy()
		{
			if (Enemy is null || !Enemy)
			{
				return false;
			}

			var isNearEnough = Vector3.Distance(transform.position, EnemyPosition) <= _shotDistance;
			var isAimed = IsAimed(EnemyPosition);
			return isNearEnough && isAimed;
		}

		public void RotateTurret(Vector3 targetPoint)
		{
			_turretController.Aim(targetPoint);
		}

		public bool IsAimed(Vector3 targetPoint)
		{
			return _turretController.Aim(targetPoint);
		}

		public void Shoot()
		{
			_gun.Shoot(Team);
		}

		private void Paint(Color color)
		{
			foreach (var detail in _detailsToPaint)
			{
				foreach (var material in detail.materials)
				{
					if (material.name.Contains("TeamColor"))
					{
						material.color = color;
					}
				}
			}
		}
	}
}