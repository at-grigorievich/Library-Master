using UnityEngine;

namespace BookLogic
{
    public class Book : MonoBehaviour, IWeightable
    {
        [Range(1,20)]
        [SerializeField] private int _weight;

        public int Weight => _weight;
    }
}