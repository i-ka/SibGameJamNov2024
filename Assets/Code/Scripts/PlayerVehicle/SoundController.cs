using System.Collections.Generic;
using UnityEngine;


namespace FS.Gameplay.PlayerVehicle
{
    public class SoundController : MonoBehaviour
    {
        [Header("Engine")]
        [SerializeField] private AudioSource engineSound;
        [SerializeField] private float minimalPitch, maximalPitch, currentPitch;
        [SerializeField] private float minimalVolume, maximalVolume, currentVolume;
        [SerializeField] private float changeRate = 1;

        private List<AudioSource> vehicleSounds = new List<AudioSource>();

        public void Init()
        {
            vehicleSounds.Add(engineSound);
            engineSound.loop = true;
        }

        public void SetPitchToEngineSound(float input)
        {
            if (input != 0)
            {
                currentPitch = Mathf.Lerp(currentPitch, maximalPitch, Time.deltaTime * changeRate);
                currentVolume = Mathf.Lerp(currentVolume, maximalVolume, Time.deltaTime * changeRate);
            }
            else if (input == 0)
            {
                currentPitch = Mathf.Lerp(currentPitch, minimalPitch, Time.deltaTime * changeRate);
                currentVolume = Mathf.Lerp(currentVolume, minimalVolume, Time.deltaTime * changeRate);
            }

            engineSound.pitch = currentPitch;
            engineSound.volume = currentVolume;
        }

        public void EnableSound()
        {
            foreach (var sound in vehicleSounds)
            {
                sound.enabled = true;
            }
        }

        public void DisableSound()
        {
            foreach (var sound in vehicleSounds)
            {
                sound.enabled = false;
            }
        }
    }
}
