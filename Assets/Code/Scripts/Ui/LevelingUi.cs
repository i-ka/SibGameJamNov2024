using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Ui;
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
        public ValueBar CurrentLevelProgres { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI FactoryLevelText { get; private set; }

        [field:SerializeField]
        public ValueBar FactoryCurrentLevelProgress { get; private set; }
    }
}


