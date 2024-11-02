using Code.Scripts.HealthSystem;
using UnityEngine;
using SibGameJam.HUD;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FS.Gameplay.PlayerVehicle
{
    public class VehicleController : MonoBehaviour
    {
        #region Components

        [Header("Custom Components")]
        [SerializeField] private MovementController movementController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private InputController inputController;
        [SerializeField] private HealthController healthController;
        [SerializeField] private PlayerHUD playerHUD;
        #endregion

        #region Variables

        private bool enableInput;

        #endregion

        #region Properties

        #endregion


        #region Initialize

        private void Awake()
        {
            AddComponents();

            if (CheckComponents())
            {
                movementController.Init();
                cameraController.Init();
                inputController.Init();
                healthController.Init();
                playerHUD.Init();

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

            return true;
        }

        #endregion

        #region Update Methods

        private void Update()
        {
            UIUpdate();

            if (!enableInput) return;

            MovementUpdate();
            CameraUpdate();

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

        private void CameraUpdate()
        {
            cameraController.SetInput(inputController.GetLeftMouseButtonHold(), inputController.GetMouseAxis(), inputController.GetMouseScroll());
        }

        #endregion

        #region Fixed Update Methods

        private void FixedUpdate()
        {
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


