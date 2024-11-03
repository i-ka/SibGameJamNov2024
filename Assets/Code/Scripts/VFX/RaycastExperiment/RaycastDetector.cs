using System;
using UnityEngine;
using UnityEngine.Events;

public class RaycastDetector : MonoBehaviour
{
    public float rayDistance = 10f;        
    public LayerMask targetLayer;          

    public event Action<RaycastHit> OnRaycastHit;

    void Update()
    {
        
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;

       if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, targetLayer))
        {
            OnRaycastHit?.Invoke(hit);
            
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green);
        }
        
    }
}
