using UnityEngine;
using UnityEngine.VFX;
//[RequireComponent(typeof(Collider))]

public class ElectricWeldingControl : MonoBehaviour
{

    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private Collider targetCollider;
    public KeyCode keyCodeStart;
    public KeyCode keyCodeStop;
    public string StartEvent = "Event Name";
    public string StopEvent = "Event Name";
    
    private void Update() 
    {
         if (Input.GetKeyDown(keyCodeStart))
         {
            
            if (IsCollidingWithTarget()) //вот здесь хотелось бы проверять наш ли это танк
            {
                PlayEffect();
            }
            
         }
         
         if (Input.GetKeyDown(keyCodeStop)) //этот инпут можно заменить на таймер, по истечении которого вызовется StopEffect()
         {
            StopEffect();
         }
    }

    

     private bool IsCollidingWithTarget()
    {
        // Проверяем, пересекается ли текущий объект с целевым
        
        
        Collider myCollider = GetComponent<Collider>();
        return myCollider != null && targetCollider != null && myCollider.bounds.Intersects(targetCollider.bounds);
    }

    public void PlayEffect()
    {
        visualEffect.SendEvent(StartEvent);
        Debug.LogWarning("Start Event Sent");
    }

    public void StopEffect()
    {
        visualEffect.SendEvent(StopEvent);
        Debug.LogWarning("Stop Event Sent");
    }
}
