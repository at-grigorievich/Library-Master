using System;
using ATG.LevelControl;
using BookLogic;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerInputService : MonoBehaviour, IInputable
    {
        private Camera _camera;
        
        public event EventHandler<ShelfBookArgs> OnStartTouch;
        public event EventHandler<Vector3> OnTouching;
        public event EventHandler OnEndTouch;

        private Vector3 _lastTouchPosition;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector3 touchPosition = touch.position;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        TrySelectBook(touchPosition);
                        break;
                    case TouchPhase.Ended:
                        TouchingEnd();
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        TouchingProcess(touchPosition);
                        break;
                }
            }
        }

        
        private void TrySelectBook(Vector3 touchPosition)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.TryGetComponent(out IShelf shelf))
                {
                    var arg = new ShelfBookArgs(shelf.RemoveBook(), shelf);
                    OnStartTouch?.Invoke(this,arg);
                }
            }
        }

        private void TouchingProcess(Vector3 touchPosition)
        {
            if (Vector3.Distance(_lastTouchPosition, touchPosition) > Mathf.Epsilon)
            {
                OnTouching?.Invoke(this, touchPosition);
                _lastTouchPosition = touchPosition;
            }
        }

        private void TouchingEnd() => OnEndTouch?.Invoke(this, null);
    }
}