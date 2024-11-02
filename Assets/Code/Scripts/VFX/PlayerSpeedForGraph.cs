using UnityEngine;
using UnityEngine.VFX;

public class PlayerSpeedForGraph : MonoBehaviour
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