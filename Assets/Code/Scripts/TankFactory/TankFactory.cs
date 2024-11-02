using System.Collections;
using System.Collections.Generic;
using SibGameJam;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TankFactory : MonoBehaviour
{
    [SerializeField]
    private List<TestTankMove> _producedTankPrefabs = new List<TestTankMove>();
    [SerializeField]
    private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField]
    private float _productionTime = 5;

    private int _currentProducedTankIndex = 0;
    private int _currentSpawnPointIndex = 0;
    private float _currentProductionProgress = 0;

    private IObjectResolver _objectResolver;
    private TankManager _tankManager;

    [Inject]
    public void Construct(IObjectResolver objectResolver, TankManager tankManager)
    {
        _objectResolver = objectResolver;
        _tankManager = tankManager;
    }

    void Update()
    {
        _currentProductionProgress += Time.deltaTime;
        if (_currentProductionProgress >= _productionTime)
        {
            SpawnTank();
            _currentProductionProgress = 0;
        }
    }

    private void SpawnTank()
    {
        Debug.Log($"Spawn tank prefab {_currentProducedTankIndex} on spawnpoint index {_currentSpawnPointIndex}");
        var prefabToInstantiate = _producedTankPrefabs[_currentProducedTankIndex];
        var spawnPoint = _spawnPoints[_currentSpawnPointIndex];

        var spawnedTank = _objectResolver.Instantiate<TestTankMove>(prefabToInstantiate, spawnPoint.position, Quaternion.identity);
        _tankManager.RegisterTank(spawnedTank);

        _currentProducedTankIndex = (_currentProducedTankIndex + 1) % _producedTankPrefabs.Count;
        _currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % _spawnPoints.Count;
    }
}
