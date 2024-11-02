using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFactory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _producedTankPrefabs = new List<GameObject>();
    [SerializeField]
    private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField]
    private float _productionTime = 5;

    private int _currentProducedTankIndex = 0;
    private int _currentSpawnPointIndex = 0;
    private float _currentProductionProgress = 0;

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

        GameObject.Instantiate(prefabToInstantiate, spawnPoint.position, Quaternion.identity);

        _currentProducedTankIndex = (_currentProducedTankIndex + 1) % _producedTankPrefabs.Count;
        _currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % _spawnPoints.Count;
    }
}
