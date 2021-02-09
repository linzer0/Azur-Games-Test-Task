using UnityEngine;

namespace Gameplay
{
    public class AnimalHolder : MonoBehaviour
    {
        [SerializeField] private FoodInformation foodInformation;

        public FoodInformation FoodInformation
        {
            get => foodInformation;
            set => foodInformation = value;
        }

        public (int, int) CurrentPosition;
    }
}