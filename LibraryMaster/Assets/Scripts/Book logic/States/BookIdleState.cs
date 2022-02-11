using ATGStateMachine;

namespace BookLogic.States
{
    public class BookIdleState: BaseStatement<IMovable>
    {
        public BookIdleState(IMovable mainObject, IStateSwitcher stateSwitcher) 
            : base(mainObject, stateSwitcher)
        {
        }

        public override void Enter()
        {
            MainObject.OnIdle();
        }

        public override void Execute()
        {
            if (MainObject.MovableStatus == MovableStatus.StartMoving)
            {
                StateSwitcher.StateSwitcher<BookMovingState>();
            }
        }
    }
}