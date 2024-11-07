using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.Gameplay.PlayerVehicle
{
    public class InputController : MonoBehaviour
    {
        #region Variables

        private Vector2 driveAxis;
        private Vector2 mouseAxis;
        private float mouseScroll;

        private bool leftMouseButtonHold;
        private bool autoDestroyButtonHold;

        #endregion

        #region Properties

        public Vector2 GetDriveAxis()
        {
            driveAxis =
                new Vector2(Input.GetAxis("Vertical"),
                Input.GetAxisRaw("Horizontal"));

            return driveAxis;
        }

        public Vector2 GetMouseAxis()
        {
            mouseAxis =
                new Vector2(Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"));

            return mouseAxis;
        }

        public float GetMouseScroll() 
        {
            mouseScroll = Input.GetAxis("Mouse ScrollWheel");
            return mouseScroll;
        }

        public bool GetLeftMouseButtonHold()
        {
            leftMouseButtonHold = Input.GetButton("Fire1");
            return leftMouseButtonHold;
        }

        public bool GetAutoDestroyButton()
        {
            autoDestroyButtonHold = Input.GetButtonDown("AutoDestroyButton");
            return autoDestroyButtonHold;
        }

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
        }

        private bool CheckComponents()
        {
            return true;
        }

        #endregion
    }
}