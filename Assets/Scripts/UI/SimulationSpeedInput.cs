using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SimulationSpeedInput : MonoBehaviour
    {
        [SerializeField] private Slider Slider;

        void Start()
        {
            Slider.onValueChanged.AddListener(UpdateSimulationSpeed);
        }

        void UpdateSimulationSpeed(float sliderValue)
        {
            GameSettings.SimulationSpeed = (int) sliderValue;
        }
    }
}