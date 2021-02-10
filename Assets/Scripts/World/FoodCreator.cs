using System.Collections.Generic;
using Animals;
using UnityEngine;
using Random = System.Random;

namespace World
{
    public class FoodCreator : MonoBehaviour
    {
        [SerializeField] private GameObject FoodPrefab;

        private Random random;


        public void CreateFood(ref List<AnimalHolder> animals)
        {
            random = new Random(UnityEngine.Random.Range(1, 10000));
            for (int i = 0; i < animals.Count; i++)
            {
                _CreateFood(i, ref animals);
            }
        }

        public void _CreateFood(int index, ref List<AnimalHolder> animals)
        {
            var foodPositionOnMap = GetFoodPosition(animals[index].CurrentPosition);

            GameSettings.Map[foodPositionOnMap.Item1, foodPositionOnMap.Item2] = 1;

            var spawnPosition = new Vector3(foodPositionOnMap.Item1 * GameSettings.TileOffset, 0,
                foodPositionOnMap.Item2 * GameSettings.TileOffset);
            var foodGameObject = Instantiate(FoodPrefab, spawnPosition, Quaternion.identity);


            (int, int ) defaultTuple = default;
            if (animals[index].MoveTo.AnimalPosition == defaultTuple)
            {
                animals[index].MoveTo.AnimalPosition = animals[index].CurrentPosition;
            }

            animals[index].MoveTo.FoodPositionOnMap = foodPositionOnMap;
            animals[index].FoodObject = foodGameObject;
        }

        private (int, int) GetFoodPosition((int, int) animalPosition)
        {
            int iterations = 5;
            int mapSize = GameSettings.MapSize;

            var potentialPosition = (random.Next(0, mapSize - 1), random.Next(0, mapSize - 1));
            int distanceToPosition = Helper.DistanceBetweenTwoDots(potentialPosition, animalPosition);

            while (distanceToPosition / GameSettings.AnimalSpeed > GameSettings.MaxTimeToFood &&
                   GameSettings.Map[potentialPosition.Item1, potentialPosition.Item2] != 1)
            {
                potentialPosition = (random.Next(0, mapSize - 1), random.Next(0, mapSize - 1));
                iterations--;
                if (iterations <= 0)
                {
                    break;
                }
            }

            return potentialPosition;
        }
    }
}