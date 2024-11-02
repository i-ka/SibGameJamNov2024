using System;
using UnityEngine;

namespace SibGameJam.TankFactory
{
    public class Resource : MonoBehaviour
    {
        [field: SerializeField]
        public ResourceType Type { get; private set; }

        [field: SerializeField]
        public int Count { get; private set; }


        /// <summary>
        /// Method to run some effects on resource and schedule object destroy
        /// </summary>
        public void Collect()
        {
            Destroy(gameObject);
        }
    }
}
