using System;
using ATGStateMachine;
using DG.Tweening;
using UnityEngine;

namespace BookLogic.States
{
    public class BookMovingState: BaseStatement<IMovable>
    {
        private Action _onMove;
        
        public BookMovingState(IMovable mainObject, IStateSwitcher stateSwitcher) 
            : base(mainObject, stateSwitcher)
        {
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
            Transform book = MainObject.Transform;
            Vector3 targetPos = MainObject.FuturePosition;
            targetPos.z = book.position.z;
            
            float speed = MainObject.ParametersData.Speed;

            book.position = Vector3.MoveTowards(book.position, targetPos, speed * Time.deltaTime);
        }
    }
}