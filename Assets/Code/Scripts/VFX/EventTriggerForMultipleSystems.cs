using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Collider))]

public class EventTriggerForMultipleSystems : MonoBehaviour
{

    public List<VisualEffect> vfxSystems;
    public string eventName = "OnHit";
    
    private void OnTriggerEnter(Collider other) 
    {
        SendEventToVFX();
    }
    public void SendEventToVFX()
    {
        foreach (var vfx in vfxSystems)
        {
            if (vfx != null)
            {
                vfx.SendEvent(eventName);
            }
        }
    }
}


