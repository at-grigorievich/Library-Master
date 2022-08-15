using System;
using ATGStateMachine;
using DG.Tweening;
using UnityEngine;

namespace BookLogic.States
{
    public class BookMovingState: BaseStatement<IMovable>
    {
        private Action _onMove;

        private readonly Transform _book;
        
        public BookMovingState(IMovable mainObject, IStateSwitcher stateSwitcher) 
            : base(mainObject, stateSwitcher)
        {
           _book = MainObject.Transform;
        }

        public override void Enter()
        {
            Vector3 boostPosition = MainObject.Transform.position
                                    + MainObject.ParametersData.BoostDelta;

            MainObject.Transform
                .DOMove(boostPosition, 1 / MainObject.ParametersData.BoostSpeed)
                .OnComplete(() => _onMove = MoveToTouch)
                ;
        }

        public override void Execute()
        {
            if (MainObject.MovableStatus != MovableStatus.EndMoving)
            {
                _onMove?.Invoke();
            }
            else
            {
                StateSwitcher.StateSwitcher<BookEndMovingState>();
            }
        }

        public override void Exit()
        {
            _onMove = null;
        }

        private void MoveToTouch()
        {
            Vector3 targetPos = MainObject.FuturePosition;
            
            var position = _book.position;
            
            targetPos.z = position.z;
            
            float speed = MainObject.ParametersData.Speed;

            position = Vector3.MoveTowards(position, targetPos, speed * Time.deltaTime);
            _book.position = position;
        }
    }
}