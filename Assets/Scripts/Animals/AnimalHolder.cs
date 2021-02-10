using UnityEngine;

namespace Animals
{
    public class AnimalHolder : MonoBehaviour
    {
        [SerializeField] private MoveTo moveTo;

        public bool FoodFound => MoveTo.FoodFound;

        public MoveTo MoveTo
        {
            get => moveTo;
            set => moveTo = value;
        }

        public GameObject FoodObject;
        public (int, int) CurrentPosition;

        void Start()
        {
            MoveTo.animalHolder = this;
        }


        public void StartSimulation()
        {
            if (FoodFound)
            {
                // MoveTo.FoodFound = false;
                Destroy(FoodObject);
                // CreateFood(index);
            }
            else
            {
                MoveTo.CalculateNextPosition();
            }
        }
    }
}