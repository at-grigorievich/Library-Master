using UnityEngine;

namespace BookLogic
{
    public class Book : MonoBehaviour, IWeightable
    {
        [Range(1,20)]
        [SerializeField] private int _weight;
        [Space(5)]
        [SerializeField] private float _thickness;
        
        public int Weight => _weight;
        public float Thickness => _thickness;
    }
}