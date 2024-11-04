using System.Collections;
using System.Linq;
using UnityEngine;


namespace SibGameJam.TankFactorySpace
{
    public class ResourceDroppoint : MonoBehaviour
    {
        [SerializeField]
        private FactoryResourceBag _factoryResourceBag;

        private void OnTriggerEnter(Collider other)
        {
            var storage = other.transform.GetComponent<PlayerResourceBag>();
            if (storage is null)
                return;

            storage.BeginUnload(_factoryResourceBag.Resources);
        }

        private void OnTriggerExit(Collider other)
        {
            var storage = other.transform.GetComponent<PlayerResourceBag>();
            if (storage is null)
                return;

            storage.EndUnload();
        }
    }

}
