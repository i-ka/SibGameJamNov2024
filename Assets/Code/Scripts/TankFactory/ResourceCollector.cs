using System;
using System.Linq;
using Code.Scripts.TankFactorySpace;
using UnityEngine;

namespace SibGameJam.TankFactorySpace
{
    /// <summary>
    /// Implements logic allowing player to collect resources
    /// </summary>
    public class ResourceCollector : MonoBehaviour
    {
        public event Action<Resource, ResourceCollector> OnResourceCollectionStarted;
        public event Action<ResourceCollector> OnResourceCollectionTerminated;
        public event Action<ResourceCollector> OnResourceCollected;
        public event Action<float, ResourceCollector> OnResourceColelectionProgressChanged;

        [SerializeField] private float _collectionRadius = 5;
        [SerializeField] private float _collectionSpeed = 0.01f;
        [SerializeField] private Transform _collectorPosition;
        [SerializeField] private LayerMask _resourcesMask;
        [SerializeField] private PlayerResourceBag _resourceBag;

        private float _collectionTimer = 0;
        private Resource _currentResource;

        private Vector3 CollectorPosition => _collectorPosition?.position ?? transform.position;


        private void FixedUpdate()
        {
            if (_currentResource != null)
            {
                ProcessResourceCollection();
                return;
            }

            var collisions = Physics.OverlapSphere(CollectorPosition, _collectionRadius, _resourcesMask);
            if (collisions.Length == 0)
                return;

            Debug.Log("Start resource collection");

            var resourceToCollect = collisions[0].gameObject.GetComponent<Resource>();
            _currentResource = resourceToCollect;
            OnResourceCollectionStarted?.Invoke(_currentResource, this);
        }

        private void ProcessResourceCollection()
        {
            var resourcePosition = _currentResource.transform.position;
            var sqrDistanceToResource = (resourcePosition - CollectorPosition).magnitude;
            if (sqrDistanceToResource > _collectionRadius)
            {
                Debug.Log($"Player out of resource range {sqrDistanceToResource} of {_collectionRadius}");
                _currentResource = null;
                _collectionTimer = 0;
                OnResourceCollectionTerminated?.Invoke(this);
                return;
            }

            _collectionTimer += Time.fixedDeltaTime;
            OnResourceColelectionProgressChanged?.Invoke(_collectionTimer / _collectionSpeed, this);
            if (_collectionTimer >= _collectionSpeed)
            {
                Debug.Log("Resource collected");
                var isResourceCollected = _resourceBag.Resources.AddResource(_currentResource.Type, _currentResource.Count);
                if (isResourceCollected)
                {
                    _currentResource.Collect();
                    OnResourceCollected?.Invoke(this);
                };
                _currentResource = null;
                _collectionTimer = 0;
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(CollectorPosition, _collectionRadius);
        }

    }
}