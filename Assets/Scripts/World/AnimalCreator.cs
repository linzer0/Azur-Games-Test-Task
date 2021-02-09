﻿using System.Collections.Generic;
using Animals;
using Gameplay;
using UnityEngine;
using Random = System.Random;

namespace World
{
    public class AnimalCreator : MonoBehaviour
    {
        [SerializeField] private GameObject AnimalPrefab;
        private int AnimalsAmount;
        private int TileOffset = 10;

        private int MapSize;

        public List<AnimalHolder> CreateAnimals(int amount, ref int[,] map)
        {
            var animalHolderList = new List<AnimalHolder>();
            var random = new Random();

            AnimalsAmount = amount;
            MapSize = map.GetLength(0);

            for (int i = 0; i < AnimalsAmount;)
            {
                var position = (random.Next(0, MapSize - 1), random.Next(0, MapSize - 1));
                if (map[position.Item1, position.Item2] != 2)
                {
                    map[position.Item1, position.Item2] = 2;

                    var spawnPosition = new Vector3(position.Item1 * TileOffset, 0, position.Item2 * TileOffset);
                    var animalGameObject = Instantiate(AnimalPrefab, spawnPosition, Quaternion.identity);

                    var animalHolder = animalGameObject.GetComponent<AnimalHolder>();
                    animalHolder.CurrentPosition = position;

                    animalHolderList.Add(animalHolder);

                    i++;
                }
            }

            return animalHolderList;
        }
    }
}