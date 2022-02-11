using System;
using ATG.LevelControl;
using BookLogic;
using Vector3 = UnityEngine.Vector3;

public class ShelfBookArgs
{
    public readonly IMovable Movable;
    public readonly IShelf Shelf;

    public ShelfBookArgs(IMovable m,IShelf s)
    {
        Movable = m;
        Shelf = s;
    }
}

namespace PlayerLogic
{
    public interface IInputable
    {
        public event EventHandler<ShelfBookArgs> OnStartTouch;
        public event EventHandler<Vector3> OnTouching;
        public event EventHandler OnEndTouch;
    }
}