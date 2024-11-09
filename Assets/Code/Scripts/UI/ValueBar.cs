using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Scripts.Ui
{
    public class ValueBar: MonoBehaviour
    {
        [SerializeField]
        private float _value;
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateImage();
            }
        }
        [SerializeField]
        private float _maxValue;
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                UpdateImage();
            }
        }
        [SerializeField]
        private float _minValue;
        public float MinValue
        {
            get => _minValue;
            set
            {
                _minValue = value;
                UpdateImage();
            }
        }
        
        [SerializeField]
        private Image image;

        private void UpdateImage()
        {
            var totalValue = _maxValue - _minValue;
            image.fillAmount = _value / totalValue;
        }
    }
}