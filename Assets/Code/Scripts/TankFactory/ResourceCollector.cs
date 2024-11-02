using System;
using System.Linq;
using UnityEngine;

namespace SibGameJam.TankFactory
{
    public class ResourceCollector : MonoBehaviour
    {
        public event Action<Resource, ResourceCollector> OnResourceCollectionStarted;
        public event Action<ResourceCollector> OnResourceCollectionTerminated;
        public event Action<ResourceCollector> OnResourceCollected;
        public event Action<float, ResourceCollector> OnResourceColelectionProgressChanged;

        [SerializeField] private float _collectionRadius = 5;
        [SerializeField] private float _collectionSpeed = 0.01f;
        [SerializeField] private LayerMask _resourcesMask;
        [SerializeField] private PlayerResourceBag _resourceBag;

        private float _collectionTimer = 0;
        private Resource _currentResource;


        private void FixedUpdate()
        {
            if (_currentResource != null)
            {
                var resourcePosition = _currentResource.transform;
                var sqrDistanceToResource = (resourcePosition.position - transform.position).sqrMagnitude / 2;
                if (sqrDistanceToResource > Mathf.Pow(_collectionRadius, 2))
                {
                    Debug.Log($"Player out of resource range {sqrDistanceToResource} of {Mathf.Pow(_collectionRadius, 2)}");
                    _currentResource = null;
                    _collectionTimer = 0;
                    OnResourceCollectionTerminated?.Invoke(this);
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
                return;
            }

            var collisions = Physics.OverlapSphere(transform.position, _collectionRadius, _resourcesMask);
            if (collisions.Length == 0)
                return;

            Debug.Log("Start resource collection");

            var resourceToCollect = collisions[0].gameObject.GetComponent<Resource>();
            _currentResource = resourceToCollect;
            OnResourceCollectionStarted?.Invoke(_currentResource, this);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _collectionRadius);
        }

    }
}