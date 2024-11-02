using Code.Scripts.AI.Data;
using Code.Scripts.AI.Movement;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


namespace Code.Scripts.AI.Brain
{
	public class Tank : MonoBehaviour
	{
		[SerializeField] private Team _team;
		[SerializeField] private MovementController _movementController;

		[SerializeField] private float _shotDistance;

		private StateMachine _stateMachine;

		private bool _seeEnemy;
		private Tank _enemyTank;

		private StateFactory _stateFactory;

		public Vector3 EnemyTankPosition => _enemyTank.transform.position;

		private Team Team => _team;

		private void Awake()
		{
			_stateFactory = new(this);
			_stateMachine ??= new();
			_stateMachine.SetState(_stateFactory.GetState(StateType.Move));
		}

		private void Update()
		{
			_stateMachine.Update();
		}

		public void MoveToPosition(Vector3 position)
		{
			_movementController.MoveToTargetPosition(position);
		}

		public void Idle()
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

			return Vector3.Distance(transform.position, EnemyTankPosition) < _shotDistance;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team)
			{
				_enemyTank = tank;
				_seeEnemy = true;
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team)
			{
				_enemyTank = tank;
				_seeEnemy = true;
			}
			else
			{
				_enemyTank = null;
				_seeEnemy = false;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<Tank>(out var tank) && tank.Team != Team)
			{
				_enemyTank = null;
				_seeEnemy = false;
			}
		}
	}
}