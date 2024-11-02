using UnityEngine;

public class FireLightFlicker : MonoBehaviour
{
   [SerializeField] private Light fireLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        fireLight = GetComponent<Light>();
    }

    void Update()
    {
        if (fireLight != null)
        {
            fireLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * flickerSpeed, 0.0f));
        }
    }
}
