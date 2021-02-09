using UnityEngine;

namespace Animals
{
    public class AnimalHolder : MonoBehaviour
    {
        [SerializeField] private MoveTo moveTo;

        public MoveTo MoveTo
        {
            get => moveTo;
            set => moveTo = value;
        } 


        public (int, int) CurrentPosition;
    }
}