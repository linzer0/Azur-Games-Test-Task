using System.Collections.Generic;
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
        [SerializeField] private AnimalCreator AnimalCreator;
        [SerializeField] private FoodCreator FoodCreator;

        private int[,] GameMap;
        //0 - free
        //1 - food
        //2 - animal
        
        private List<AnimalHolder> AnimalHolder = new List<AnimalHolder>();
        


        void Start()
        {
            StartButton.onClick.AddListener(GetValuesFromSlider);
        }

        void GetValuesFromSlider()
        {
            MapSize = (int) MapSizeSlider.value;
            AnimalsAmount = (int) AnimalsAmountSlider.value;
            Speed = (int) SpeedValueSlider.value;
            
            GameMap = MapCreator.CreateMap(MapSize);
            
            AnimalHolder = AnimalCreator.CreateAnimals(AnimalsAmount, ref GameMap);
            FoodCreator.CreateFood(ref GameMap, ref AnimalHolder, Speed);
            
            StartButton.onClick.RemoveListener(GetValuesFromSlider);
        }
    }
}