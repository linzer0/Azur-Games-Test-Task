using UnityEngine;

namespace Gameplay
{
    public class AnimalHolder : MonoBehaviour
    {
        [SerializeField] private FoodInformation FoodInformation;

        public (int, int) CurrentPosition;
    }
}