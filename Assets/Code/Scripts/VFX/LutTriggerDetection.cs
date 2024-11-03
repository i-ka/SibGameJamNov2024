using UnityEngine;
[RequireComponent(typeof(Collider))]        // Collider have to be as a trigger
[RequireComponent(typeof(Rigidbody))]



public class LutTriggerDetection : MonoBehaviour
{
    public LutHighlighting sineChanger; // Ссылка на скрипт, который меняет материал
    public LayerMask targetLayer;            // Слой, для которого будет работать триггер

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект, попавший в триггер, находится в targetLayer
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            // Запускаем синусоидальное изменение параметра
            sineChanger.StartSineWave();

            Debug.Log("Объект вошел в триггер: " + other.name);
        
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Проверяем, если объект, покидающий триггер, находится в targetLayer
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            // Останавливаем синусоидальное изменение параметра
            sineChanger.StopSineWave();

            Debug.Log("Объект покинул триггер: " + other.name);
        
        }
    }
}
