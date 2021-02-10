using System.Collections.Generic;
using Other;
using UnityEngine;

namespace Animals
{
    public class MoveTo : MonoBehaviour
    {
        public AnimalHolder animalHolder;
        public (int, int) FoodPositionOnMap;
        public (int, int) AnimalPosition;

        public bool MovingIsAllowed;
        public bool FoodFound;


        private Vector3 NextMovingPosition;
        private float DeltaDistance = 0.25f;

        void Update()
        {
            if (!MovingIsAllowed) return;

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
                    OnFoodFound();
                }
                else
                {
                    CalculateNextPosition();
                }

            }
        }

        private void OnFoodFound()
        {
            FoodFound = true;
            animalHolder.OnFoodFound();
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
                MovingIsAllowed = true;
            }
        }

        private (int, int) GetNearestNextPosition()
        {
            var neighbourPositions = new List<(int, int)>()
            {
                (AnimalPosition.Item1 + 1, AnimalPosition.Item2), (AnimalPosition.Item1 - 1, AnimalPosition.Item2),
                (AnimalPosition.Item1, AnimalPosition.Item2 + 1), (AnimalPosition.Item1, AnimalPosition.Item2 - 1)
            };

            Helper.Shuffle(neighbourPositions);

            int minimalElementIndex = -1,
                minimalDistanceToAnotherPosition = 100;

            for (int i = 0; i < neighbourPositions.Count; i++)
            {
                if (neighbourPositions[i].Item1 >= 0 && neighbourPositions[i].Item1 < GameSettings.MapSize &&
                    (neighbourPositions[i].Item2 >= 0 && neighbourPositions[i].Item2 < GameSettings.MapSize))
                {
                    if (GameSettings.Map[neighbourPositions[i].Item1, neighbourPositions[i].Item2] != 2)
                    {
                        var distanceToPosition = Helper.DistanceBetweenTwoDots(FoodPositionOnMap, neighbourPositions[i]);
                        if (minimalDistanceToAnotherPosition >= distanceToPosition)
                        {
                            minimalDistanceToAnotherPosition = distanceToPosition;
                            minimalElementIndex = i;
                        }
                    }
                }
            }

            return minimalElementIndex == -1 ? AnimalPosition : neighbourPositions[minimalElementIndex];
        }
    }
}