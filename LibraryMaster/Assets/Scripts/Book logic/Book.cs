using System;
using ATGStateMachine;
using BookLogic.States;
using UnityEngine;

namespace BookLogic
{
    public class Book : StatementBehaviour<IMovable>, IWeightable, IMovable
    {
        [Range(1,20)]
        [SerializeField] private int _weight;
        [Space(5)]
        [SerializeField] private float _thickness;
        
        public int Weight => _weight;
        public float Thickness => _thickness;
        
        public MovableStatus MovableStatus { get; private set; } = MovableStatus.Idle;
        
        public Vector3 PreviousPlacePosition { get; private set; }
        public Vector3 FuturePosition { get; private set; }

        
        private void Awake()
        {
            AllStates.Add(new BookIdleState(this,this));
            AllStates.Add(new BookMovingState(this,this));
            
            InitStartState();
            OnState();
        }

        private void Update()
        {
            OnExecute();
        }

        
        public void OnIdle()
        {
            MovableStatus = MovableStatus.Idle;
        }

        public void OnStartMoving()
        {
            MovableStatus = MovableStatus.StartMoving;
            PreviousPlacePosition = transform.position;
        }

        public void OnMoving(Vector3 position)
        {
            FuturePosition = position;
        }

        public void OnEndMoving()
        {
            MovableStatus = MovableStatus.EndMoving;
        }
    }
}