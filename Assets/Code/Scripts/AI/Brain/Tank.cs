using Code.Scripts.AI.Controllers;
using Code.Scripts.AI.Data;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


namespace Code.Scripts.AI.Brain
{
	public class Tank : MonoBehaviour
	{
		[SerializeField] private Team _team;
		[SerializeField] private MovementController _movementController;
		[SerializeField] private TurretController _turretController;
		[SerializeField] private Gun _gun;

		[SerializeField] private Transform _baseTransform;

		[SerializeField] private float _shotDistance;

		private StateMachine _stateMachine;

		private bool _seeEnemy;
		private Tank _enemyTank;

		private StateFactory _stateFactory;

		public Transform BaseTransform
		{
			get => _baseTransform;
			set { _baseTransform = value; }
		}

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
			if (_enemyTank is null)
			{
				return false;
			}

			var isNearEnough = Vector3.Distance(transform.position, EnemyTankPosition) <= _shotDistance;
			var isAimed = IsAimed(EnemyTankPosition);
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
				_enemyTank = null;
			}
		}
	}
}