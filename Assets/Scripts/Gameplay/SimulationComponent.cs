using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SimulationComponent : MonoBehaviour
    {
        private int MapSize;
        private int AnimalsAmount;
        private int Speed;

        [SerializeField] private Button StartButton;

        [SerializeField] private Slider MapSizeSlider;
        [SerializeField] private Slider AnimalsAmountSlider;
        [SerializeField] private Slider SpeedValueSlider;

        [SerializeField] private MapCreator MapCreator;


        void Start()
        {
            StartButton.onClick.AddListener(GetValuesFromSlider);
        }

        void GetValuesFromSlider()
        {
            MapSize = (int) MapSizeSlider.value;
            AnimalsAmount = (int) AnimalsAmountSlider.value;
            Speed = (int) SpeedValueSlider.value;
            
            MapCreator.CreateMap(MapSize);

            StartButton.onClick.RemoveListener(GetValuesFromSlider);
        }
    }
}