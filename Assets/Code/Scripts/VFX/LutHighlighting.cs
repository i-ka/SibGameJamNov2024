using UnityEngine;

public class LutHighlighting : MonoBehaviour
{
    public Renderer targetRenderer;           // Рендер объекта
    public string propertyName = "_YourProperty";  // Имя параметра материала
    public float defValue = 0f;
    public float minValue = 0.1f;               // Минимальное значение параметра
    public float maxValue = 0.25f;               // Максимальное значение параметра
    public float speed = 3f;                  // Скорость изменения значения

    private MaterialPropertyBlock _propBlock;
    private float _time;
    private bool _isRunning = false;          // Флаг для запуска/остановки изменения параметра

    void Start()
    {
        _propBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        if (_isRunning)
        {
            // Обновляем время с учётом скорости
            _time += Time.deltaTime * speed;

            // Рассчитываем текущее значение параметра по синусоиде
            float value = Mathf.Lerp(minValue, maxValue, (Mathf.Sin(_time) + 1) / 2f);

            // Устанавливаем значение параметра через MaterialPropertyBlock
            targetRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat(propertyName, value);
            targetRenderer.SetPropertyBlock(_propBlock);
        }

        else 
        {
            targetRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat(propertyName, defValue);
            targetRenderer.SetPropertyBlock(_propBlock);
        }
    }

    // Метод для запуска изменения параметра
    public void StartSineWave()
    {
        _isRunning = true;
        _time = 0;  // Сброс времени, чтобы начать с начала
    }

    // Метод для остановки изменения параметра
    public void StopSineWave()
    {
        _isRunning = false;
    }
}
