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
		[SerializeField] private int _damage;
		[SerializeField] private Transform _turretTransform;
		[SerializeField] private Vector2 _gunSpread;
		[SerializeField] private AudioSource _shotSound;

		private PoolMono<Projectile> _projectilePool;
		private Transform _poolContainer;

		private float _lastShotTime;

		public void Initialize(Transform poolContainer)
		{
			_poolContainer = poolContainer;
			_projectilePool = new(_bulletPrefab, 5, _poolContainer);
		}

		public void SetDamage(int damage)
		{
			_damage = damage;
		}

		public void Shoot(Team team)
		{
			if (Time.time - _lastShotTime < _reloadingTime)
			{
				return;
			}

			_shotSound.pitch = Random.Range(0.9f, 1.1f);
			_shotSound.PlayOneShot(_shotSound.clip);
            float xSpread = Random.Range(-_gunSpread.x, _gunSpread.x);
            float ySpread = Random.Range(-_gunSpread.y, _gunSpread.y);

            Vector3 spreadDirection = new Vector3(xSpread, ySpread);

            var bullet = _projectilePool.GetFreeElement();
			bullet.SetDamage(_damage);
			bullet.SetSpread(spreadDirection);
			bullet.transform.SetPositionAndRotation(_bulletSpawnPointTransform.position, Quaternion.LookRotation(_bulletSpawnPointTransform.forward, _bulletSpawnPointTransform.up));
			bullet.EnemyTeam = team == Team.Red ? Team.Blue : Team.Red;
			bullet.SetSpeed(_bulletSpeed);
			_lastShotTime = Time.time;
		}
	}
}