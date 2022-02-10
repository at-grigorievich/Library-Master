using System;
using ATG.LevelControl;
using UnityEngine;

namespace Input_Logic
{
    public class InputService : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        CastMovable(touch.position);
                        break;
                }
            }
        }

        private void CastMovable(Vector3 touchPosition)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.TryGetComponent(out IShelf shelf))
                {
                    Debug.Log("asfasfasfasf");
                }
            }
        }
    }
}