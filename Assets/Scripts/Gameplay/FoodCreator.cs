using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class FoodCreator : MonoBehaviour
    {
        [SerializeField] private GameObject FoodPrefab;

        private int MaxTimeToFood = 5;
        private int TileOffset = 10;

        public void CreateFood(ref int[,] map, ref List<AnimalHolder> animals, int Speed)
        {
            for (int i = 0; i < animals.Count;)
            {
                Random.InitState(i);
                int mapSize = map.GetLength(0);

                var position = (Random.Range(0, mapSize - 1), Random.Range(0, mapSize - 1));

                var distanceToPosition = Math.Abs(position.Item1 - animals[i].CurrentPosition.Item1) +
                                         Mathf.Abs(position.Item1 - animals[i].CurrentPosition.Item2);

                Debug.Log($"Distance to Food is {distanceToPosition}");

                if (distanceToPosition * TileOffset / Speed <= MaxTimeToFood)
                {
                    if (map[position.Item1, position.Item2] != 1)
                    {
                        map[position.Item1, position.Item2] = 1;
                        animals[i].FoodInformation.FoodPosition = position;
                        
                        var spawnPosition = new Vector3(position.Item1 * TileOffset, 0, position.Item2 * TileOffset);
                        var foodGameObject = Instantiate(FoodPrefab, spawnPosition, Quaternion.identity);
                        
                    }
                }
                
                i++;
            }
        }

        private (int, int) FoodPosition()
        {
            return (0, 0);
        }
    }
}