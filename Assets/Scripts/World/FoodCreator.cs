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
            int mapSize = GameSettings.MapSize;

            var position = (random.Next(0, mapSize - 1), random.Next(0, mapSize - 1));

            
            //TODO ADD LOGIC 5 second moving
            var distanceToPosition = Math.Abs(position.Item1 - animals[index].CurrentPosition.Item1) +
                                     Mathf.Abs(position.Item1 - animals[index].CurrentPosition.Item2);

            GameSettings.Map[position.Item1, position.Item2] = 1;

            var spawnPosition = new Vector3(position.Item1 * GameSettings.TileOffset, 0,
                position.Item2 * GameSettings.TileOffset);
            var foodGameObject = Instantiate(FoodPrefab, spawnPosition, Quaternion.identity);

            //REFACTOR

            (int, int ) defaultTuple = default;
            if (animals[index].MoveTo.AnimalPosition == defaultTuple)
            {
                animals[index].MoveTo.AnimalPosition = animals[index].CurrentPosition;
            }

            animals[index].MoveTo.FoodPositionOnMap = position;
            animals[index].FoodObject = foodGameObject;
        }
    }
}