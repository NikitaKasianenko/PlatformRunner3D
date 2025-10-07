using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class Settings
    {
        [SerializeField] private Slider sensitivitySlider;
        [SerializeField] private float sensitivity = 1f;

        private void Start()
        {
            sensitivitySlider.value = sensitivity;

            sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        }

        private void OnSensitivityChanged(float value)
        {
            sensitivity = value;
        }

        public float GetSensitivity()
        {
            return sensitivity;
        }
    }
}