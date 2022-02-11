using ATG.LevelControl;
using ATGStateMachine;
using UnityEngine;

namespace BookLogic.States
{
    public class BookEndMovingState: BaseStatement<IMovable>
    {
        public BookEndMovingState(IMovable mainObject, IStateSwitcher stateSwitcher) 
            : base(mainObject, stateSwitcher)
        {
        }
        
        public override void Enter()
        {
            CastShelf();
        }


        private void CastShelf()
        {
            BookParametersContainer values = MainObject.ParametersData;

            float maxDistance = values.BoxCastDistance;

            RaycastHit hit;
            
            Vector3 zDelay = values.BoxCastSize.z / 2f * Vector3.forward;

            var transform = MainObject.Transform;
            
            bool isHit = Physics.BoxCast(transform.position + zDelay , values.BoxCastSize / 2, -transform.up, out hit,
                transform.rotation, maxDistance);

            if (isHit)
            {
                if (hit.transform.TryGetComponent(out IShelf shelf))
                {
                    Debug.Log("asfasfasf");
                }
            }
            else
            {
                StateSwitcher.StateSwitcher<BookCancelMovingState>();
            }
        }
    }
}