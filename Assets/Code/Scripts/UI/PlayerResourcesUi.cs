using TMPro;
using UnityEngine;

namespace Code.Scripts.Ui
{
    public class PlayerResourcesUi : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI CurrentResourcesText { get; private set; }
    }
}