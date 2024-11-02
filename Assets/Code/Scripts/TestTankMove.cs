using System;
using SibGameJam;
using UnityEngine;

public class TestTankMove : MonoBehaviour, ITank
{
    public event Action<ITank> OnDestroyed;

    private float _destroyTimer = 0;

    public Side Side => Side.Enemy;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 5);
        _destroyTimer += Time.deltaTime;
        if (_destroyTimer >= 2)
        {
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

    }


}