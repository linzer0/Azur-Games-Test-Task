using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OnSliderChange : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private Slider Slider;

        void Start()
        {
            Slider.onValueChanged.AddListener(UpdateText);
        }

        void UpdateText(float sliderValue)
        {
            Text.SetText(sliderValue.ToString());
        }
    }
}