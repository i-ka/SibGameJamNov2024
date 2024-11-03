using UnityEngine;

public class RaycastHandler : MonoBehaviour
{
    public RaycastDetector raycastDetector; 
    public LutHighlighting lutHighlighting;

    void Start()
    {
        
        if (raycastDetector != null)
        {
            raycastDetector.OnRaycastHit += HandleRaycastHit;
        }
    }

    
    private void HandleRaycastHit(RaycastHit hit)
    {
        
        lutHighlighting.StartSineWave();
        //Debug.Log("Объект обнаружен: " + hit.collider.name);

        
    }

    void OnDestroy()
    {
        
        if (raycastDetector != null)
        {
            raycastDetector.OnRaycastHit -= HandleRaycastHit;
        }
    }
}
