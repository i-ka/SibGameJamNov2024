using System;
using System.Collections.Generic;
using System.Linq;

namespace SibGameJam.TankFactory
{
    public class ResourceHolder
    {
        public IReadOnlyDictionary<ResourceType, int> Resources => _resources;
        public int? Capacity { get; private set; }
        private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

        public event Action<ResourceType, int, int> OnResourceCountChanged;

        public ResourceHolder(int? capacity = null)
        {
            Capacity = capacity;
        }

        public bool AddResource(ResourceType type, int count)
        {
            if (Capacity is not null && Capacity < _resources.Values.Sum() + count)
                return false;

            if (!_resources.ContainsKey(type))
                _resources[type] = count;
            else
                _resources[type] += count;

            OnResourceCountChanged?.Invoke(type, _resources[type], count);

            return true;
        }

        public bool RemoveResource(ResourceType type, int count)
        {
            if (!_resources.ContainsKey(type))
                return false;

            if (_resources[type] < count)
                return false;

            _resources[type] -= count;

            OnResourceCountChanged?.Invoke(type, _resources[type], -count);

            return true;
        }
    }

}
