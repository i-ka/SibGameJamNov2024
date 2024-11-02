using System;
using Code.Scripts.AI.Brain.States;
using Code.Scripts.AI.Controllers;
using Code.Scripts.AI.Data;
using SibGameJam;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


namespace Code.Scripts.AI.Brain
{
	public class Tank : MonoBehaviour, ITank
	{
		[SerializeField] private Team _team;
		[SerializeField] private MovementController _movementController;
		[SerializeField] private TurretController _turretController;

		[SerializeField] private float _shotDistance;

		private StateMachine _stateMachine;

		private bool _seeEnemy;
		private Tank _enemyTank;

		private StateFactory _stateFactory;

        public event Action<ITank> OnDestroyed;

        public Team Team => _team;

		public StateMachine StateMachine => _stateMachine;
		public StateFactory StateFactory => _stateFactory;
		public Vector3 EnemyTankPosition => _enemyTank.transform.position;

        private void Awake()
		{
			_stateFactory = new(this);
			_stateMachine ??= new();
			_stateMachine.SetState(_stateFactory.GetState(StateType.Movement));
			//StateMachine.SetState(new MovementState(this));
		}

		private void Update()
		{
			_stateMachine.Update();
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
			if (!_enemyTank)
			{
				return false;
			}
			return Vector3.Distance(transform.position, EnemyTankPosition) <= _shotDistance;
		}

		public void RotateTurret(Vector3 targetPoint)
		{
			_turretController.RotateTurret(targetPoint);
		}

		public bool IsAimed(Vector3 targetPoint)
		{
			return _turretController.RotateTurret(targetPoint);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team)
			{
				_enemyTank = tank;
				_seeEnemy = true;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team)
			{
				_seeEnemy = false;
			}
		}
	}
}