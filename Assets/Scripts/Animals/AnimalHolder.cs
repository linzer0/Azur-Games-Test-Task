using UnityEngine;
using World;

namespace Animals
{
    public class AnimalHolder : MonoBehaviour
    {
        [SerializeField] private MoveTo moveTo;

        public SimulationCreator SimulationCreator;

        public bool FoodFound
        {
            get => MoveTo.FoodFound;
            private set => MoveTo.FoodFound = value;
        }

        public MoveTo MoveTo => moveTo;
        public GameObject FoodObject;
        public (int, int) CurrentPosition;

        private void Start()
        {
            MoveTo.animalHolder = this;
        }

        public void OnFoodFound()
        {
            SimulationCreator.OnFoodFound(this);
            FoodFound = false;
        }
    }
}