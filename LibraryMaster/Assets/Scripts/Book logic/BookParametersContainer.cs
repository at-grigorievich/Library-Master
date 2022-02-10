using UnityEngine;

namespace BookLogic
{
    [CreateAssetMenu(fileName = "Book parameters", menuName = "Book/New Book Values", order = 0)]
    public class BookParametersContainer : ScriptableObject
    {
        [Range(0.1f, 25f)] 
        [SerializeField] private float _speed;
        [Space(15)]
        [Range(1f, 50f)] 
        [SerializeField] private float _boostSpeed;
        [SerializeField] private Vector3 _boostDelta;

        public float Speed => _speed;
        public float BoostSpeed => _boostSpeed;

        public Vector3 BoostDelta => _boostDelta;
    }
}