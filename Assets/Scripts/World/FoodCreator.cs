using System;
using System.Collections.Generic;
using Animals;
using UnityEngine;
using Random = System.Random;

namespace World
{
    public class FoodCreator : MonoBehaviour
    {
        [SerializeField] private GameObject FoodPrefab;


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


                if (distanceToPosition * GameSettings.TileOffset / Speed <= GameSettings.MaxTimeToFood)
                {
                    if (map[position.Item1, position.Item2] != 1)
                    {
                        map[position.Item1, position.Item2] = 1;
                        
                        var spawnPosition = new Vector3(position.Item1 * GameSettings.TileOffset, 0, position.Item2 * GameSettings.TileOffset);
                        var foodGameObject = Instantiate(FoodPrefab, spawnPosition, Quaternion.identity);

                        //REFACTOR
                        animals[i].MoveTo.AnimalPosition = animals[i].CurrentPosition;
                        animals[i].MoveTo.FoodPositionOnMap = position;
                        animals[i].MoveTo.FoodPosition = spawnPosition;
                        animals[i].MoveTo.Map = map;

                        
                        i++;
                    }
                }
            }
        }
    }
}