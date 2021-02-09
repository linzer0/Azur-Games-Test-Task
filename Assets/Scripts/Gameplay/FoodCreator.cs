using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Gameplay
{
    public class FoodCreator : MonoBehaviour
    {
        [SerializeField] private GameObject FoodPrefab;

        private int MaxTimeToFood = 5;
        private int TileOffset = 10;

        public void CreateFood(ref int[,] map, ref List<AnimalHolder> animals, int Speed)
        {
            var random = new Random();

            for (int i = 0; i < animals.Count;)
            {
                int mapSize = map.GetLength(0);

                var position = (random.Next(0, mapSize - 1), random.Next(0, mapSize - 1));
                //CAN BE REFACTORED

                var distanceToPosition = Math.Abs(position.Item1 - animals[i].CurrentPosition.Item1) +
                                         Mathf.Abs(position.Item1 - animals[i].CurrentPosition.Item2);


                if (distanceToPosition * TileOffset / Speed <= MaxTimeToFood)
                {
                    if (map[position.Item1, position.Item2] == 0)
                    {
                        map[position.Item1, position.Item2] = 1;
                        animals[i].FoodInformation.FoodPosition = position;

                        var spawnPosition = new Vector3(position.Item1 * TileOffset, 0, position.Item2 * TileOffset);
                        var foodGameObject = Instantiate(FoodPrefab, spawnPosition, Quaternion.identity);
                        i++;
                    }
                }

            }
        }
    }
}