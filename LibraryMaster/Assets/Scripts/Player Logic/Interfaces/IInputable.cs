using System;
using System.Numerics;
using BookLogic;
using Vector3 = UnityEngine.Vector3;

namespace PlayerLogic
{
    public interface IInputable
    {
        public event EventHandler<IMovable> OnStartTouch;
        public event EventHandler<Vector3> OnTouching;
        public event EventHandler OnEndTouch;
    }
}