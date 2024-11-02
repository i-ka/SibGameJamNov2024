using SibGameJam.TankFactory;
using UnityEngine;

public class FactoryResourceBag : MonoBehaviour
{
    public ResourceHolder Resources { get; private set; }

    private void Awake()
    {
        Resources = new ResourceHolder();
    }
}