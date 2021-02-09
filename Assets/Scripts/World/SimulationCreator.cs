using System.Collections.Generic;
using Animals;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace World
{
    public class SimulationCreator : MonoBehaviour
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
            GameSettings.AnimalSpeed = Speed;
            
            GameMap = MapCreator.CreateMap(MapSize);
            
            AnimalHolder = AnimalCreator.CreateAnimals(AnimalsAmount, ref GameMap);
            FoodCreator.CreateFood(ref AnimalHolder);

            // StartButton.onClick.RemoveListener(GetValuesFromSlider);
        }


        public void StartSimulation()
        {
            for (var index = 0; index < AnimalHolder.Count; index++)
            {
                var animalHolder = AnimalHolder[index];
                if (!animalHolder.FoodFound)
                {
                    animalHolder.MoveTo.CalculateNextPosition();
                }
                else
                {
                    animalHolder.MoveTo.FoodFound = false;
                    AnimalHolder[index].CurrentPosition = animalHolder.CurrentPosition;
                    Destroy(animalHolder.FoodObject);
                    CreateFood(index);
                }
            }
        }

        private void CreateFood(int index)
        {
           FoodCreator._CreateFood(index, ref AnimalHolder);
        }
    }
}