using Code.Scripts.AI.Brain;
using Code.Scripts.AI.Data;
using Code.Scripts.Pool;
using UnityEngine;

namespace Code.Scripts.AI.Controllers
{
	public class Gun : MonoBehaviour
	{
		[SerializeField] private Transform _bulletSpawnPointTransform;
		[SerializeField] private Projectile _bulletPrefab;
		[SerializeField] private float _bulletSpeed;
		[SerializeField] private float _reloadingTime;
		[SerializeField] private Transform _poolContainer;
		[SerializeField] private Transform _turretTransform;

		private PoolMono<Projectile> _projectilePool;

		private float _lastShotTime;

		private void Awake()
		{
			_projectilePool = new(_bulletPrefab, 5, _poolContainer);
		}

		public void Shoot(Team team)
		{
			if (Time.time - _lastShotTime < _reloadingTime)
			{
				return;
			}

			var bullet = _projectilePool.GetFreeElement();
			bullet.transform.position = _bulletSpawnPointTransform.position;
			bullet.transform.rotation = _turretTransform.rotation;
			bullet.EnemyTeam = team == Team.Red ? Team.Blue : Team.Red;
			bullet.SetSpeed(_bulletSpeed);
			_lastShotTime = Time.time;
		}
	}
}