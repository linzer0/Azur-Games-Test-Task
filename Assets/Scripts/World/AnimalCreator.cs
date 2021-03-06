﻿using System.Collections.Generic;
using Animals;
using Other;
using UnityEngine;
using Random = System.Random;

namespace World
{
    public class AnimalCreator : MonoBehaviour
    {
        [SerializeField] private GameObject AnimalPrefab;

        private int AnimalsAmount;
        
        public SimulationCreator SimulationCreator;
        
        public List<AnimalHolder> CreateAnimals(int amount)
        {
            var animalHolderList = new List<AnimalHolder>();
            var random = new Random();

            AnimalsAmount = amount;

            for (int i = 0; i < AnimalsAmount;)
            {
                var position = (random.Next(0, GameSettings.MapSize - 1), random.Next(0, GameSettings.MapSize - 1));
                if (GameSettings.Map[position.Item1, position.Item2] != 2)
                {
                    GameSettings.Map[position.Item1, position.Item2] = 2;

                    var spawnPosition = new Vector3(position.Item1 * GameSettings.TileOffset, 0,
                        position.Item2 * GameSettings.TileOffset);
                    var animalGameObject = Instantiate(AnimalPrefab, spawnPosition, Quaternion.identity);

                    var animalHolder = animalGameObject.GetComponent<AnimalHolder>();
                    animalHolder.CurrentPosition = position;
                    animalHolder.SimulationCreator = SimulationCreator;

                    animalHolderList.Add(animalHolder);

                    i++;
                }
            }

            return animalHolderList;
        }
    }
}