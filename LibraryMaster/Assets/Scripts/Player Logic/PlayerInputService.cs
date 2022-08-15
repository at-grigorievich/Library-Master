using System;
using System.Collections;
using ATG.LevelControl;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PlayerLogic
{
    public class PlayerInputService : MonoBehaviour, IInputable
    {
        [Inject] private ILevelStatus _levelStatus;

        private Camera _camera;
        private PlayerInput _playerInput;

        private Action _tryToMoveSelected;

        public event EventHandler<ShelfBookArgs> OnStartInput;
        public event EventHandler<Vector3> OnInput;
        public event EventHandler OnEndInput;


        private IEnumerator Start()
        {
            _camera = Camera.main;

            _playerInput = new PlayerInput();

            yield return new WaitForSeconds(.5f);
            
            _playerInput.Player.Select.performed += DetectStartLevel;
            _playerInput.Enable();
        }

        private void Update() => _tryToMoveSelected?.Invoke();

        private void OnDisable()
        {
            _playerInput.Player.Select.performed -= SelectBook;
            _playerInput.Player.Unselect.performed -= UnselectBook;

            _tryToMoveSelected = null;
            
            _playerInput.Disable();
        }

        private void DetectStartLevel(InputAction.CallbackContext obj)
        {
            _playerInput.Player.Select.performed -= DetectStartLevel;

            _playerInput.Player.Select.performed += SelectBook;
            _playerInput.Player.Unselect.performed += UnselectBook;

            _tryToMoveSelected += TouchingProcess;

            _levelStatus.StartLevel();
        }

        private void SelectBook(InputAction.CallbackContext obj)
        {
            var inputPosition = _playerInput.Player.Position.ReadValue<Vector2>();

            if (!Physics.Raycast(_camera.ScreenPointToRay(inputPosition), out var hit)) 
                return;

            if (!hit.transform.TryGetComponent(out IShelf shelf)) return;

            if (!shelf.ContainsBook) return;
            
            var arg = new ShelfBookArgs(shelf.RemoveBook(), shelf);
            OnStartInput?.Invoke(this, arg);
        }
        private void UnselectBook(InputAction.CallbackContext obj) =>
            OnEndInput?.Invoke(this, null);
        private void TouchingProcess() => 
            OnInput?.Invoke(this, _playerInput.Player.Position.ReadValue<Vector2>());
    }
}