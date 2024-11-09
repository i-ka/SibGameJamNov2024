using SibGameJam;
using SibGameJam.ScriptableObjects.PlayerBonuses;
using System.Collections.Generic;
using UnityEngine;
using VContainer;


namespace Code.Gameplay.PlayerVehicle
{
    public class VehicleSoundController : MonoBehaviour
    {
        [Header("Engine")]
        [SerializeField] private AudioSource engineSound;
        [SerializeField] private float minPitchMoving, maxPitchMoving;
        [SerializeField] private float minVolumeMoving, maxVolumeMoving;
        [SerializeField] private float soundRateMoving = 1;

        [Header("Events")]
        [SerializeField] private AudioSource _levelUpSound;
        [SerializeField] private AudioSource _repairSound;
        [SerializeField] private AudioSource _unloadSound;
        [SerializeField] private AudioSource _collectSound;


        private float currentVolumeMoving, currentPitchMoving;

        private List<AudioSource> vehicleSounds = new List<AudioSource>();
        [Inject]private ResearchManager _researchManager;

        public void Init()
        {
            vehicleSounds.Add(engineSound);
            vehicleSounds.Add(_levelUpSound);
            engineSound.loop = true;
        }

        private void Start()
        {
            
        }

        [Inject]
        public void Construct(ResearchManager researchManager)
        {
            _researchManager = researchManager;
            _researchManager.OnLevelUp += PlayLevelUpSound;
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

        private void PlayLevelUpSound(int level, PlayerBonus[] playerBonuses)
        {
            _levelUpSound.PlayOneShot(_levelUpSound.clip);
            
        }

        public void PlayTankRepairSound()
        {
            _repairSound.PlayOneShot(_repairSound.clip);
        }

        public void PlayUnloadBagSound()
        {
            _unloadSound.PlayOneShot(_unloadSound.clip);
        }

        public void PlayResourceCollectedSound()
        {
            _collectSound.pitch = UnityEngine.Random.Range(0.98f, 1.02f);
            _collectSound.PlayOneShot(_collectSound.clip);
        }
    }
}
