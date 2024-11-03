using SibGameJam.GameServices;
using SibGameJam.TankFactory;
using UnityEngine;
using VContainer;

public class FactoryResourceBag : MonoBehaviour
{
    public ResourceHolder Resources { get; private set; }

    private FactoryUpgradeManager _factoryUpdateManager;

    [Inject]
    public void Construct(FactoryUpgradeManager factoryUpgradeManager)
    {
        _factoryUpdateManager = factoryUpgradeManager;
    }

    private void Awake()
    {
        Resources = new ResourceHolder();
        Resources.OnResourceCountChanged += AddResearchPoints;
    }


    private void AddResearchPoints(ResourceType type, int currentCount, int delta)
    {
        Debug.Log("Adding research points");
        if (delta <= 0)
            return;

        _factoryUpdateManager.AddUpgradePoints(type, delta);
    }
}