using Code.Scripts.AI.Data;
using Code.Scripts.Pool;
using UnityEngine;

public class TankPoolAI : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private TankShell shellPrefab;

    [Header("Variables")]
    [SerializeField] private int poolCount = 5;
    [SerializeField] private bool autoExpand = true;

    private PoolMono<TankShell> shellPool;

    private void Awake()
    {
        shellPool = new PoolMono<TankShell>(shellPrefab, poolCount, transform);
        shellPool.AutoExpand = autoExpand;
    }

    public void CreateShell(Transform spawnPoint)
    {
        var newShell = shellPool.GetFreeElement();

        newShell.transform.position = spawnPoint.position;
        newShell.transform.rotation = Quaternion.LookRotation(spawnPoint.forward, spawnPoint.up);
    }
}
