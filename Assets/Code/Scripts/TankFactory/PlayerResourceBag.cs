using System.Linq;
using SibGameJam.TankFactorySpace;
using UnityEngine;

/// <summary>
/// Represents player interactions with storages
/// </summary>

// TODO think about addin some interface
public class PlayerResourceBag : MonoBehaviour
{

    [SerializeField]
    private float _unloadTime;
    private float _unloadTimer;
    private ResourceHolder _unloadTarget;

    public ResourceHolder Resources { get; private set; }

    [SerializeField]
    private int _capacity = 5;

    /// <summary>
    /// Start resource unload
    /// </summary>
    /// <param name="target">Holder where resources will be unloaded</param>
    /// <returns>True if unload was started</returns>
    public bool BeginUnload(ResourceHolder target)
    {
        if (_unloadTarget != null)
            return false; //already unloading somewhere

        _unloadTarget = target;

        return true;
    }

    /// <summary>
    /// Stops running unload
    /// </summary>
    public void EndUnload()
    {
        _unloadTarget = null;
        _unloadTimer = 0;
    }

    private void Awake()
    {
        Resources = new ResourceHolder(_capacity);
    }

    private void Update()
    {
        ProcessUnload();
    }

    private void ProcessUnload()
    {
        if (_unloadTarget == null) return;

        _unloadTimer += Time.deltaTime;
        if (_unloadTimer >= _unloadTime)
            MakeUnload();
    }

    private void MakeUnload()
    {
        foreach (var resouceType in Resources.Resources.Keys.ToArray())
        {
            var count = Resources.Resources[resouceType];
            Resources.RemoveResource(resouceType, count);
            _unloadTarget.AddResource(resouceType, count);

            Debug.Log($"Transfered resource {count} {resouceType}");
        }

        _unloadTarget = null;
        _unloadTimer = 0;
    }

}