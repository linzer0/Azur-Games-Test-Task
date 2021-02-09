using System.Collections.Generic;
using UnityEngine;

namespace Animals
{
    public class MoveTo : MonoBehaviour
    {
        private float DeltaDistance = 0.25f;

        //TODO Get from input
        public int Speed = 10;

        public Vector3 FoodPosition;
        public (int, int) FoodPositionOnMap;
        public int[,] Map;

        public (int, int) AnimalPosition;

        public bool Start = false;


        private Vector3 NextMovingPosition;
        private bool FoodFound = false;

        void Update()
        {
            if (Start)
            {
                if (Vector3.Distance(transform.position, NextMovingPosition) > DeltaDistance)
                {
                    transform.position =
                        Vector3.MoveTowards(transform.position, NextMovingPosition,
                            GameSettings.AnimalSpeed * GameSettings.SimulationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = NextMovingPosition;

                    if (AnimalPosition == FoodPositionOnMap)
                    {
                        Debug.Log("We're found food");
                        FoodFound = true;
                        //TODO
                        //CREATE NEW FOOD
                    }

                    Start = false;
                }
            }
        }

        public void CalculateNextPosition()
        {
            if (!FoodFound)
            {
                GameSettings.Map[AnimalPosition.Item1, AnimalPosition.Item2] = 0;
                AnimalPosition = GetNearestNextPosition();
                GameSettings.Map[AnimalPosition.Item1, AnimalPosition.Item2] = 2;

                NextMovingPosition = new Vector3(AnimalPosition.Item1 * GameSettings.TileOffset, 0,
                    AnimalPosition.Item2 * GameSettings.TileOffset);
                Start = true;
            }
        }

        private (int, int) GetNearestNextPosition()
        {
            List<(int, int)> neighbourPositions = new List<(int, int)>()
            {
                (AnimalPosition.Item1 + 1, AnimalPosition.Item2), (AnimalPosition.Item1 - 1, AnimalPosition.Item2),
                (AnimalPosition.Item1, AnimalPosition.Item2 + 1), (AnimalPosition.Item1, AnimalPosition.Item2 - 1)
            };

            Helper.Shuffle(neighbourPositions);

            int minimalElementIndex = -1;
            int minimalDistanceToAnotherPosition = 100;

            for (int i = 0; i < neighbourPositions.Count; i++)
            {
                if (neighbourPositions[i].Item1 >= 0 && neighbourPositions[i].Item1 < Map.GetLength(0) &&
                    (neighbourPositions[i].Item2 >= 0 && neighbourPositions[i].Item2 < Map.GetLength(0)))
                {
                    if (GameSettings.Map[neighbourPositions[i].Item1, neighbourPositions[i].Item2] != 2)
                    {
                        var distanceToPosition = DistanceBetweenTwoDots(FoodPositionOnMap, neighbourPositions[i]);
                        if (minimalDistanceToAnotherPosition >= distanceToPosition)
                        {
                            minimalDistanceToAnotherPosition = distanceToPosition;
                            minimalElementIndex = i;
                        }
                    }
                }
            }

            if (minimalElementIndex == -1)
            {
                return AnimalPosition;
            }

            return neighbourPositions[minimalElementIndex];
        }

        private int DistanceBetweenTwoDots((int, int) first, (int, int) second)
        {
            return Mathf.Abs(first.Item1 - second.Item1) + Mathf.Abs(first.Item2 - second.Item2);
        }
    }
}