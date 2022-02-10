using UnityEngine;

namespace BookLogic
{
    public enum MovableStatus
    {
        Idle,
        StartMoving,
        EndMoving
    }
    
    public interface IMovable
    {
        MovableStatus MovableStatus { get; }
        Vector3 PreviousPlacePosition { get; }
        Vector3 FuturePosition { get; }

        void OnIdle();
        void OnStartMoving();
        void OnMoving(Vector3 position);
        void OnEndMoving();
    }
}