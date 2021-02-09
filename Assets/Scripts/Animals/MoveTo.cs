using UnityEngine;

namespace Animals
{
    public class MoveTo : MonoBehaviour
    {
        private int DeltaDistance = 1;
        public int Speed = 10;

        public Vector3 NextPosition;
        public (int, int) PositionOnMap;

        public bool Start = false;

        void Update()
        {
            if (Start)
            {
                if (Vector3.Distance(transform.position, NextPosition) > DeltaDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, NextPosition, Speed * Time.deltaTime);
                }
            }
        }
    }
}