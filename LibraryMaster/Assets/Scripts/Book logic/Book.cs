using System;
using ATG.LevelControl;
using ATGStateMachine;
using BookLogic.States;
using UnityEngine;

namespace BookLogic
{
    public class Book : StatementBehaviour<IMovable>, IWeightable, IMovable
    {
        [SerializeField] private BookParametersContainer _bookParameters;
        [Space(10)]
        [Range(1,20)]
        [SerializeField] private int _weight;
        [Space(5)]
        [SerializeField] private float _thickness;
        
        public int Weight => _weight;
        public float Thickness => _thickness;
        
        public BookParametersContainer ParametersData => _bookParameters;
        public Transform Transform => transform;

        public MovableStatus MovableStatus { get; private set; } = MovableStatus.Idle;
        
        public Vector3 PreviousPlacePosition { get; private set; }
        public IShelf PreviousShelf { get; private set; }
        
        public Vector3 FuturePosition { get; private set; }

        
        private void Awake()
        {
            AllStates.Add(new BookIdleState(this,this));
            AllStates.Add(new BookMovingState(this,this));
            AllStates.Add(new BookEndMovingState(this,this));
            AllStates.Add(new BookCancelMovingState(
                () => PreviousShelf.TryAddBook(this),
                this,this));
            
            InitStartState();
            OnState();
        }

        private void Update()
        {
            OnExecute();
        }

        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
                float maxDistance = ParametersData.BoxCastDistance;

                RaycastHit hit;

                Vector3 zDelay = ParametersData.BoxCastSize.z / 2f * Vector3.forward;
                bool isHit = Physics.BoxCast(transform.position + zDelay , ParametersData.BoxCastSize / 2, -transform.up, out hit,
                    transform.rotation, maxDistance);

                if (isHit)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(transform.position,-transform.up*hit.distance);
                    Gizmos.DrawWireCube(transform.position-transform.up*hit.distance,ParametersData.BoxCastSize);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(transform.position,-transform.up*maxDistance);
                }
            #endif
        }

        public void OnIdle()
        {
            MovableStatus = MovableStatus.Idle;
        }

        public void OnStartMoving(IShelf prvShelf)
        {
            MovableStatus = MovableStatus.StartMoving;

            PreviousShelf = prvShelf;
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