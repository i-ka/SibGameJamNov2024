using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.Gameplay.PlayerVehicle
{
    public class CameraController : MonoBehaviour
    {
        #region Components

        [SerializeField] private Transform target;
        [SerializeField] private Transform camera;

        #endregion

        #region Variables

        private bool isMouseHold;
        private Vector2 mouseInput;
        private float mouseScroll;

        [Header("Config")]
        [SerializeField] private float targetdistance = 5.0f;
        [SerializeField] private float xSpeed = 120.0f;
        [SerializeField] private float ySpeed = 120.0f;
        [SerializeField] private float scrollSensitivity = 4f;
        [SerializeField] private float smoothnessX = 0.1f;
        [SerializeField] private float smoothnessY = 1f;
        [SerializeField] private float smoothnessZoom = 1f;

        [Header("Camera limits")]
        [SerializeField] private float distanceMin = 0.5f;
        [SerializeField] private float distanceMax = 15f;
        [SerializeField] private float yMinLimit = -20f;
        [SerializeField] private float yMaxLimit = 80f;

        [SerializeField] private float x, y, targetX, targetY, distance;
        #endregion

        #region Properties

        #endregion

        #region Initialize

        public void Init()
        {
            if (CheckComponents())
            {
                Debug.Log($"{gameObject.name} initialized successfully!");
            }
            else
            {
                Debug.LogError($"{gameObject.name} can't initialize!");

#if UNITY_EDITOR
                EditorApplication.isPaused = true;
#endif
            }

            Vector3 angles = camera.transform.eulerAngles;
            x = angles.y;
            y = angles.x;

        }

        private bool CheckComponents()
        {
            return true;
        }


        #endregion

        #region Update Methods

        public void SetInput(bool isMouseHold, Vector2 mouseInput, float mouseScroll)
        {
            this.isMouseHold = isMouseHold;
            this.mouseInput = mouseInput;
            this.mouseScroll = mouseScroll;
        }

        public void UpdateController()
        {
            RotateCamera();
        }

        private void RotateCamera()
        {
            if (isMouseHold)
            {
                targetX += mouseInput.x * xSpeed * 0.02f;
                targetY -= mouseInput.y * ySpeed * 0.02f;
            }
            targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);

            x = Mathf.LerpAngle(x, targetX, smoothnessX);
            y = Mathf.LerpAngle(y, targetY, smoothnessY);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            targetdistance = Mathf.Clamp(targetdistance - (mouseScroll * scrollSensitivity), distanceMin, distanceMax);
            distance = Mathf.Lerp(distance, targetdistance, smoothnessZoom); 

            Vector3 newDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * newDistance + target.position;

            camera.transform.rotation = rotation;
            camera.transform.position = position;
        }

        #endregion

        #region Editor Methods

        private void OnValidate()
        {

        }

        #endregion

        #region Static Methids

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F) angle += 360F;
            if (angle > 360F) angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        #endregion
    }
}