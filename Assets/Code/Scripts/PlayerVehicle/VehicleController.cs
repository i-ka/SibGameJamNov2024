using UnityEngine;

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
        [SerializeField] private ForkliftController forkliftController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private InputController inputController;
        [SerializeField] private SoundController soundController;

        #endregion

        #region Variables

        private bool enableInput;

        #endregion

        #region Properties

        #endregion


        #region Initialize

        private void Awake()
        {
            if (CheckComponents())
            {
                movementController.Init();
                forkliftController.Init();
                cameraController.Init();
                inputController.Init();
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

        private bool CheckComponents()
        {
            if (!movementController)
            {
                Debug.LogError($"No {movementController} in {gameObject.name}");
                return false;
            }
            if (!forkliftController)
            {
                Debug.LogError($"No {forkliftController} in {gameObject.name}");
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
            if (!enableInput) return;

            MovementUpdate();
            CameraUpdate();
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
            // custom
            movementController ??= GetComponentInChildren<MovementController>();
            forkliftController ??= GetComponentInChildren<ForkliftController>();
            cameraController ??= GetComponentInChildren<CameraController>();
            inputController ??= GetComponentInChildren<InputController>();
            soundController ??= GetComponentInChildren<SoundController>();
        }

        #endregion
    }
}


