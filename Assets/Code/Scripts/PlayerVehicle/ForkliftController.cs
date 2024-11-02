using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FS.Gameplay.PlayerVehicle
{
    public class ForkliftController : MonoBehaviour
    {
        #region Components


        #endregion

        #region Variables

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

        }

        private bool CheckComponents()
        {
            return true;
        }


        #endregion

        #region Update Methods



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

    
