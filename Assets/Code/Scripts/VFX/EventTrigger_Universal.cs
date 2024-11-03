using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(Collider))]

public class EventTriggerUniversal : MonoBehaviour
{

    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private Collider targetCollider;
    public string Event = "Event Name";

    public void PlayEffect()
    {
        visualEffect.SendEvent(Event);
    }
}
