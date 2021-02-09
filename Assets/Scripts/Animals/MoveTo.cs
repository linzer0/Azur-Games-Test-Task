using UnityEngine;

namespace Animals
{
    public class MoveTo : MonoBehaviour
    {
        private int DeltaDistance = 1;

        //TODO Get from input
        public int Speed = 10;

        public Vector3 FoodPosition;
        public (int, int) FoodPositionOnMap;
        public int[,] Map;

        public (int, int) AnimalPosition;

        public bool Start = false;


        private Vector3 NextMovingPosition;

        void Update()
        {
            if (Start)
            {
                if (Vector3.Distance(transform.position, NextMovingPosition) > DeltaDistance)
                {
                    transform.position =
                        Vector3.MoveTowards(transform.position, NextMovingPosition, Speed * Time.deltaTime);
                }
                else
                {
                    Start = false;
                    AnimalPosition = FoodPositionOnMap;
                }
            }
        }

        public void CalculateNextPosition()
        {
            var nextPosition = GetNearestNextPosition();
            NextMovingPosition = new Vector3(nextPosition.Item1 * GameSettings.TileOffset, 0,
                nextPosition.Item2 * GameSettings.TileOffset);
            Start = true;
        }

        private (int, int) GetNearestNextPosition()
        {
            var neighbourPositions = new[]
            {
                (AnimalPosition.Item1 + 1, AnimalPosition.Item2), (AnimalPosition.Item1 - 1, AnimalPosition.Item2),
                (AnimalPosition.Item1, AnimalPosition.Item2 + 1), (AnimalPosition.Item1, AnimalPosition.Item2 - 1)
            };

            int minimalIndex = 0;
            int distance = 10;

            for (int i = 0; i < neighbourPositions.Length; i++)
            {
                var distanceToPosition = DistanceBetweenTwoDots(FoodPositionOnMap, neighbourPositions[i]);
                if (distance >= distanceToPosition)
                {
                    distance = distanceToPosition;
                    minimalIndex = i;
                }
            }

            return neighbourPositions[minimalIndex];
        }

        private int DistanceBetweenTwoDots((int, int) first, (int, int) second)
        {
            return Mathf.Abs(first.Item1 - second.Item1) + Mathf.Abs(first.Item2 - second.Item2);
        }
    }
}