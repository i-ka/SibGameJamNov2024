using Code.Scripts.HealthSystem;
using UnityEngine;
using SibGameJam.HUD;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.Gameplay.PlayerVehicle
{
    public class VehicleController : MonoBehaviour
    {
        #region Components
        [Header("Configs")]
        [SerializeField] private RobotControllerConfig controllerConfig;
        [SerializeField] private RobotWheelsConfig wheelsConfig;

        [Header("Custom Components")]
        [SerializeField] private MovementController movementController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private InputController inputController;
        [SerializeField] private HealthController healthController;
        [SerializeField] private PlayerHUD playerHUD;
        [SerializeField] private PlayerAbilityController abilityController;
        [SerializeField] private SoundController soundController;
        [SerializeField] private PlayerResourceBag resourceBag;
        #endregion

        #region Variables

        private bool enableInput;
        

        #endregion

        #region Properties

        public HealthController HealthController => healthController;
        public MovementController MovementController => movementController;
        public PlayerAbilityController AbilityController => abilityController;
        public PlayerResourceBag ResourceBag => resourceBag;

        public void SetInputEnabled(bool state)
        {
            enableInput = state;
        }

        #endregion


        #region Initialize

        private void Awake()
        {
            AddComponents();

            if (CheckComponents())
            {
                movementController.Init(controllerConfig, wheelsConfig);
                cameraController.Init();
                inputController.Init();
                healthController.Init(0);
                playerHUD.Init();
                soundController.Init();

                Debug.Log($"{gameObject.name} initialized successfully!");
                Debug.Log($"{gameObject.name} ready to go!");
            }
            else
            {
                Debug.LogError($"{gameObject.name} can't initialize!");

#if UNITY_EDITOR
                EditorApplication.isPaused = true;
#endif
            }

            enableInput = true;
        }

        private void AddComponents()
        {
            movementController ??= GetComponentInChildren<MovementController>();
            cameraController ??= GetComponentInChildren<CameraController>();
            inputController ??= GetComponentInChildren<InputController>();
            healthController ??= GetComponent<HealthController>();
            playerHUD ??= GetComponentInChildren<PlayerHUD>();
            soundController ??= GetComponentInChildren<SoundController>();
        }

        private bool CheckComponents()
        {
            if (!movementController)
            {
                Debug.LogError($"No {movementController} in {gameObject.name}");
                return false;
            }
            if (!cameraController)
            {
                Debug.LogError($"No {cameraController} in {gameObject.name}");
                return false;
            }
            if (!inputController)
            {
                Debug.LogError($"No {inputController} in {gameObject.name}");
                return false;
            }
            if (!healthController)
            {
                Debug.LogError($"No {healthController} in {gameObject.name}");
                return false;
            }
            if (!playerHUD)
            {
                Debug.LogError($"No {playerHUD} in {gameObject.name}");
                return false;
            }
            if (!soundController)
            {
                Debug.LogError($"No {soundController} in {gameObject.name}");
                return false;
            }

            return true;
        }

        #endregion

        #region Update Methods

        private void Update()
        {
            UIUpdate();
            movementController.PositionAndRotationController();
            CameraUpdate();
            SoundUpdate();

            if (!enableInput) return;
            MovementUpdate();
            AutoDestroyUpdate();
        }

        private void UIUpdate()
        {
            playerHUD.SetTankSpeed(movementController.GetSpeedInKmpH);
            playerHUD.SetTankPosition(movementController.GetPosition.position);
        }

        private void MovementUpdate()
        {
            movementController.UpdateController();
            movementController.SetInput(inputController.GetDriveAxis());
        }

        private void AutoDestroyUpdate()
        {
            if (inputController.GetAutoDestroyButton())
            {
                resourceBag.ForcedUnload();
                movementController.TeleportPlayer(movementController.teleportPointAfterIncline);
            }
            
        }

        private void CameraUpdate()
        {
            cameraController.SetInput(inputController.GetLeftMouseButtonHold(), inputController.GetMouseAxis(), inputController.GetMouseScroll());
        }

        private void SoundUpdate()
        {
            soundController.SetPitchToEngineSound(inputController.GetDriveAxis().x);
        }

        #endregion

        #region Fixed Update Methods

        private void FixedUpdate()
        {
            if (!enableInput) return;
            MovementFixedUpdate();
        }

        private void MovementFixedUpdate()
        {
            movementController.Acceleration();
            movementController.Steering();
        }

        #endregion

        #region LateUpdateMethods

        private void LateUpdate()
        {
            cameraController.UpdateController();
        }

        #endregion

        #region Editor Methods

        private void OnValidate()
        {
            AddComponents();
        }

        #endregion
    }
}


