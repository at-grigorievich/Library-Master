using ATG.LevelControl;
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
        BookParametersContainer ParametersData { get; }
        
        Transform Transform { get; }
        
        MovableStatus MovableStatus { get; }
        
        Vector3 PreviousPlacePosition { get; }
        IShelf PreviousShelf { get; }
        
        Vector3 FuturePosition { get; }
        IShelf FutureShelf { get; set; }

        void OnIdle();
        void OnStartMoving(IShelf prvShelf);
        void OnMoving(Vector3 position);
        void OnEndMoving();
    }
}