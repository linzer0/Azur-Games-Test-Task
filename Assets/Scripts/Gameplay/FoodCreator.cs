using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class FoodCreator : MonoBehaviour
    {
        public void CreateFood(ref int[,] map, ref List<AnimalHolder> animals)
        {
            for (int i = 0; i < animals.Count; i++)
            {
                Random.InitState(i);
            }
        }

        private (int, int) FoodPosition()
        {
            return (0, 0);
        }
    }
}