using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Scripts.Pool
{
	public class PoolMono<T> where T : MonoBehaviour
	{
		private List<T> _pool;

		public PoolMono(T prefab, int count)
		{
			Prefab = prefab;
			Container = null;

			CreatePool(count);
		}

		public PoolMono(T prefab, int count, Transform container)
		{
			Prefab = prefab;
			Container = container;

			CreatePool(count);
		}

		private T Prefab { get; }
		public bool AutoExpand { get; set; } = true;
		private Transform Container { get; }

		private void CreatePool(int count)
		{
			_pool = new();

			for (var i = 0; i < count; i++)
			{
				CreateObject();
			}
		}

		private T CreateObject(bool isActiveByDefault = false)
		{
			var createdObject = Object.Instantiate(Prefab, Container);
			createdObject.gameObject.SetActive(isActiveByDefault);
			_pool.Add(createdObject);
			return createdObject;
		}

		private bool HasFreeElement(out T element)
		{
			foreach (var mono in _pool.Where(mono => !mono.gameObject.activeInHierarchy))
			{
				element = mono;
				mono.gameObject.SetActive(true);
				return true;
			}

			element = null;
			return false;
		}

		public T GetFreeElement()
		{
			if (HasFreeElement(out var element))
			{
				return element;
			}

			if (AutoExpand)
			{
				return CreateObject(true);
			}

			throw new($"There is now free elements if pool of type {typeof(T)}");
		}
	}
}