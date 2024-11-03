using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(Collider))]

public class EventTriggerUniversal : MonoBehaviour
{

    [SerializeField] private VisualEffect visualEffect;
    public string Event = "Event Name";
    private void OnTriggerEnter(Collider other) 
    {
        visualEffect.SendEvent(Event);
    }
}
