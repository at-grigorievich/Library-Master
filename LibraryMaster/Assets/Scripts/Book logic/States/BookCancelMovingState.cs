using System;
using ATGStateMachine;
using DG.Tweening;
using UnityEngine;

namespace BookLogic.States
{
    public class BookCancelMovingState: BaseStatement<IMovable>
    {
        private readonly Func<bool> _callback;
        
        public BookCancelMovingState(Func<bool> callback,IMovable mainObject, IStateSwitcher stateSwitcher) 
            : base(mainObject, stateSwitcher)
        {
            _callback = callback;
        }
        
        public override void Enter()
        {
            
#if UNITY_ANDROID || UNITY_IOS
            Taptic.Vibrate();
#endif
            
            Vector3 backPosition = MainObject.PreviousPlacePosition;
            backPosition.z = MainObject.Transform.position.z;

            DOTween.Sequence()
                .Append(MainObject.Transform.DOMove(backPosition, 1 / MainObject.ParametersData.BoostSpeed))
                .Append(MainObject.Transform.DOMove(MainObject.PreviousPlacePosition,
                    1 / MainObject.ParametersData.BoostSpeed))
                .OnComplete(() =>
                {
                    if (_callback())
                    {
                        StateSwitcher.StateSwitcher<BookIdleState>();
                    }
                });
        }
    }
}