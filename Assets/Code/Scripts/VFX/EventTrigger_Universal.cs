using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(Collider))]

public class EventTriggerUniversal : MonoBehaviour
{

    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private Collider targetCollider;
    public string Event = "Event Name";
    private void OnTriggerEnter(Collider other) 
    {
        if (other == targetCollider) 
        {
            visualEffect.SendEvent(Event);
        }
        
    }
}
