using ATGStateMachine;
using UnityEngine;

namespace BookLogic.States
{
    public class BookMovingState: BaseStatement<IMovable>
    {
        public BookMovingState(IMovable mainObject, IStateSwitcher stateSwitcher) : base(mainObject, stateSwitcher)
        {
        }

        public override void Enter()
        {
            Debug.Log("asfasf");
        }
    }
}