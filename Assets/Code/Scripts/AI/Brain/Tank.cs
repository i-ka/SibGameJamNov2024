using System;
using System.Collections.Generic;
using Code.Scripts.AI.Controllers;
using Code.Scripts.AI.Data;
using Code.Scripts.HealthSystem;
using SibGameJam;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;


namespace Code.Scripts.AI.Brain
{
	public class Tank : MonoBehaviour, ITank
	{
		public event Action<ITank> OnDestroyed;
		[SerializeField] private Team _team;
		[SerializeField] private MovementController _movementController;
		[SerializeField] private TurretController _turretController;
		[SerializeField] private HealthController _healthController;
		[SerializeField] private Gun _gun;
		[SerializeField] private HealthController _healthController;

		[SerializeField] private float _shotDistance;
		[SerializeField] private int _escapeThresholdHealth;
		[SerializeField] private int _healingMax;
		[SerializeField] private int _escapeEscapeZoneThreshold;

		private Transform _bulletPoolTransform;

		private bool _seeEnemy;

		private List<Transform> _escapePoints;

		public Transform BaseTransform { get; private set; }
		
		public HealthController HealthController => _healthController;

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
		public List<Transform> EscapePoints => _escapePoints;
		public int HealingMax => _healingMax;
		public int EscapeEscapeZoneThreshold => _escapeEscapeZoneThreshold;

		private void Update()
		{
			StateMachine?.Update();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team)
			{
				if (Enemy == null)
				{
					Enemy = tank.gameObject;
				}

				_seeEnemy = true;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team)
			{
				_seeEnemy = false;
				Enemy = null;
			}
		}

		public Team Team
		{
			get => _team;
			private set => _team = value;
		}

		public void Initialize(Team team, Transform baseTransform, Transform bulletsPoolContainer, List<Transform> escapePoints)
		{
			_healthController.Init();
			Team = team;
			BaseTransform = baseTransform;
			BulletPoolContainer = bulletsPoolContainer;
			_escapePoints = escapePoints;
			StateFactory = new(this);
			StateMachine ??= new();
			StateMachine.SetState(StateFactory.GetState(StateType.Movement));
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
			return _seeEnemy;
		}

		public bool CanShotEnemy()
		{
			if (Enemy is null)
			{
				return false;
			}

			var isNearEnough = Vector3.Distance(transform.position, EnemyPosition) <= _shotDistance;
			var isAimed = IsAimed(EnemyPosition);
			return isNearEnough && isAimed;
		}

		public void RotateTurret(Vector3 targetPoint)
		{
			_turretController.RotateTurret(targetPoint);
		}

		public bool IsAimed(Vector3 targetPoint)
		{
			return _turretController.RotateTurret(targetPoint);
		}

		public void Shoot()
		{
			_gun.Shoot(Team);
		}
	}
}