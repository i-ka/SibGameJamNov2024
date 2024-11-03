using System.Collections.Generic;
using UnityEngine;


namespace FS.Gameplay.PlayerVehicle
{
    public class SoundController : MonoBehaviour
    {
        [Header("Engine")]
        [SerializeField] private AudioSource engineSound;
        [SerializeField] private float minPitchMoving, maxPitchMoving, currentPitchMoving;
        [SerializeField] private float minVolumeMoving, maxVolumeMoving, currentVolumeMoving;
        [SerializeField] private float soundRateMoving = 1;

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
                currentPitchMoving =
                    Mathf.Lerp(currentPitchMoving, maxPitchMoving * Mathf.Abs(input), Time.deltaTime * soundRateMoving);
                currentVolumeMoving =
                    Mathf.Lerp(currentVolumeMoving, maxVolumeMoving * Mathf.Abs(input), Time.deltaTime * soundRateMoving);
            }
            else
            {
                currentPitchMoving =
                    Mathf.Lerp(currentPitchMoving, minPitchMoving, Time.deltaTime * soundRateMoving);
                currentVolumeMoving =
                    Mathf.Lerp(currentVolumeMoving, minVolumeMoving, Time.deltaTime * soundRateMoving);
            }

            engineSound.pitch = currentPitchMoving;
            engineSound.volume = currentVolumeMoving;
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
