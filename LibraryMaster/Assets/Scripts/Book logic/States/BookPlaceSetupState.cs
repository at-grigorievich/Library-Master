using System;
using ATGStateMachine;

namespace BookLogic.States
{
    public class BookPlaceSetupState: BaseStatement<IMovable>
    {
        private readonly Func<bool> _callack;

        public BookPlaceSetupState(Func<bool> callback,IMovable mainObject, IStateSwitcher stateSwitcher) 
            : base(mainObject, stateSwitcher)
        {
            _callack = callback;
        }
        
        public override void Enter()
        {
            bool isAlreadyPlaced = _callack();
            
            if (isAlreadyPlaced)
            {
#if UNITY_ANDROID
                Vibration.Vibrate(200);
#endif
                
                StateSwitcher.StateSwitcher<BookIdleState>();
            }
            else
            {
                StateSwitcher.StateSwitcher<BookCancelMovingState>();
            }
        }
    }
}