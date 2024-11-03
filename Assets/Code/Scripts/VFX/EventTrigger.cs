using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(Collider))]

public class EventTrigger : MonoBehaviour
{

    [SerializeField] private VisualEffect visualEffect;
    private void OnTriggerEnter(Collider other) 
    {
        visualEffect.SendEvent("OnHit");
    }
}
