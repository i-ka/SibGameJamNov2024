using UnityEngine;

public class TestTankMove: MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 5);
    }
}