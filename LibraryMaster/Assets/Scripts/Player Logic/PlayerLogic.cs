using System;
using BookLogic;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Player_Logic
{
    public class PlayerLogic : MonoBehaviour
    {
        [Inject] private IInputable _inputable;

        private IMovable _movableObject;
        
        private void Start()
        {
            _inputable.OnStartTouch += OnStartTouch;
            _inputable.OnTouching += OnTouching;
            _inputable.OnEndTouch += OnEndTouch;
        }
        
        
        private void OnStartTouch(object sender, IMovable e)
        {
            _movableObject = e;
            _movableObject.OnStartMoving();
        }
        
        private void OnEndTouch(object sender, EventArgs e)
        {
            if (_movableObject != null)
            {
                _movableObject.OnEndMoving();
            }
        }
        
        private void OnTouching(object sender, Vector3 e)
        {
            if (_movableObject != null)
            {
                _movableObject.OnMoving(e);
            }
        }
    }
}