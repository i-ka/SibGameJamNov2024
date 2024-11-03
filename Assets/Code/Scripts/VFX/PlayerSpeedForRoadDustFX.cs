using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;

public class PlayerSpeedForRoadDustFX : MonoBehaviour
{
public VisualEffect vfx;

private Vector3 previousPosition;

void Start() 

{
    previousPosition = transform.position;
}

void Update() 
{
    Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;
    vfx.SetVector3("PlayerVelocity", velocity);
    previousPosition = transform.position;
}

}