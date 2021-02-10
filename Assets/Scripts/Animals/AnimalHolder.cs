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
            set => MoveTo.FoodFound = value;
        }

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


        public void OnFoodFound()
        {
            FoodFound = false;
            Destroy(FoodObject);
            SimulationCreator.CreateFood(this);
        }
    }
}