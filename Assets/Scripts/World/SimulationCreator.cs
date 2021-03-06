﻿using System.Collections.Generic;
using Animals;
using Other;
using UnityEngine;
using UnityEngine.UI;

namespace World
{
    public class SimulationCreator : MonoBehaviour
    {
        private int MapSize;
        private int AnimalsAmount;
        private int Speed;


        [Header("UI")] 
        [SerializeField] private GameObject NewSimulationHolder;
        [SerializeField] private GameObject SimulationHolder;
        
        [SerializeField] private Button StartButton;
        
        [SerializeField] private Slider MapSizeSlider;
        [SerializeField] private Slider AnimalsAmountSlider;
        [SerializeField] private Slider SpeedValueSlider;

        [Header("Simulation")]
        [SerializeField] private MapCreator MapCreator;
        [SerializeField] private AnimalCreator AnimalCreator;
        [SerializeField] private FoodCreator FoodCreator;
        
        [Header("FX")]

        [SerializeField] private GameObject EffectPrefab;

        private List<AnimalHolder> AnimalHolderList = new List<AnimalHolder>();
        
        void Start()
        {
            StartButton.onClick.AddListener(OnStartClick);
            AnimalCreator.SimulationCreator = this;
        }

        private void OnStartClick()
        {
           GetValuesFromSlider();
           
            AnimalHolderList = AnimalCreator.CreateAnimals(AnimalsAmount);
            FoodCreator.CreateFood(ref AnimalHolderList);
            
            NewSimulationHolder.SetActive(false);
            SimulationHolder.SetActive(true);
        }

        void GetValuesFromSlider()
        {
            MapSize = (int) MapSizeSlider.value;
            AnimalsAmount = (int) AnimalsAmountSlider.value;
            Speed = (int) SpeedValueSlider.value;
            
            GameSettings.AnimalSpeed = Speed;
            GameSettings.Map = MapCreator.CreateMap(MapSize);
        }

        public void StartSimulation()
        {
            for (var index = 0; index < AnimalHolderList.Count; index++)
            {
                var animalHolder = AnimalHolderList[index];
                if (!animalHolder.FoodFound)
                {
                    animalHolder.MoveTo.CalculateNextPosition();
                }
                else
                {
                    animalHolder.MoveTo.FoodFound = false;
                    AnimalHolderList[index].CurrentPosition = animalHolder.CurrentPosition;
                    Destroy(animalHolder.FoodObject);
                    CreateFood(index);
                }
            }
        }

        private void CreateFood(int index)
        {
           FoodCreator._CreateFood(index, ref AnimalHolderList);
        }

        private void CreateFood(AnimalHolder animalHolder)
        {
            int index = AnimalHolderList.IndexOf(animalHolder);
            if (index != -1)
            {
                AnimalHolderList[index].CurrentPosition = animalHolder.CurrentPosition;
                CreateFood(index);
            }
            else
            {
                Debug.Log("Not found");
            }
        }

        public void OnFoodFound(AnimalHolder animalHolder)
        {
            CreateEffect(animalHolder.FoodObject.transform.position);
            Destroy(animalHolder.FoodObject);
            CreateFood(animalHolder);
        }

        private void CreateEffect(Vector3 effectPosition, float lifeTime = 1.0f)
        {
            if (EffectPrefab == null) return;
            
            var effectGameObject = Instantiate(EffectPrefab, effectPosition, Quaternion.identity);
            Destroy(effectGameObject, lifeTime);
        }
    }
}