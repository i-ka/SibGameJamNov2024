using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FS.Gameplay.PlayerVehicle
{
    public class WheelViewController : MonoBehaviour
    {
        #region Components

        [Header("Basic Components")]
        [SerializeField] private Transform[] wheelsModels;
        [SerializeField] private WheelCollider[] wheelsColliders;

        #endregion

        #region Variables

        #endregion

        #region Properties

        public WheelCollider[] WheelsColliders => wheelsColliders;

        #endregion

        #region Initialize

        public void Init()
        {
            if (CheckComponents())
            {
                InitWheels();
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

        private void InitWheels()
        {
            for (int i = 0; i < wheelsColliders.Length; i++)
            {
                wheelsModels[i] = wheelsColliders[i].GetComponentInChildren<MeshRenderer>().transform;
            }
        }

        private bool CheckComponents()
        {
            if (wheelsColliders.Length < 4)
            {
                Debug.LogError($"No wheels colliders in {gameObject.name}");
                return false;
            }
            return true;
        }


        #endregion

        #region Update Methods

        public void UpdateWheelsTransform()
        {
            for (int i = 0; i < wheelsColliders.Length; ++i)
            {
                var wheel = wheelsColliders[i];
                Vector3 p;
                Quaternion q;

                wheel.GetWorldPose(out p, out q);
                wheelsModels[i].transform.position = p;
                wheelsModels[i].transform.rotation = q;
            }
        }

        #endregion

        #region Fixed Update Methods

        #endregion

        #region Editor Methods

        private void OnValidate()
        {
            
        }

        #endregion
    }
}

