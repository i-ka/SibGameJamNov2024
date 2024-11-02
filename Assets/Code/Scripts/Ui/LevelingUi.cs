using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Ui
{
    public class LevelingUi : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI LevelText { get; private set; }
        
        [field:SerializeField]
        public Slider CurrentLevelProgres { get; private set; }
    }
}


