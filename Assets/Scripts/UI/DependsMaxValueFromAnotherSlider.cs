using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DependsMaxValueFromAnotherSlider : MonoBehaviour
    {
        [SerializeField] private Slider DependsFromSlider;
        [SerializeField] private Slider CurrentFromSlider;

        void Start()
        {
            DependsFromSlider.onValueChanged.AddListener(SetMaximumValue);
        }

        void SetMaximumValue(float value)
        {
            CurrentFromSlider.maxValue = (int) value * value / 2;
            CurrentFromSlider.onValueChanged.Invoke(CurrentFromSlider.value);
        }
    }
}