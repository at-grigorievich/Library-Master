using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ATG.LevelControl;
using BookLogic;
using DG.Tweening;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Player_Logic
{
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private float _defaultFOV;
        [SerializeField] private float _loopFOV;
        
        [Inject] private IInputable _inputable;
        
        [Inject] private ILevelStatus _levelStatus;
        [Inject] private ILevelSystem _levelSystem;

        [DllImport("__Internal")]
        private static extern void UpdateBodyColor(string color);
        
        private IMovable _movableObject;

        private Camera _camera;
        
        private List<IShelf> _shelfsOnLevel;
        private int _totalBookCount;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _levelStatus.OnLevelStart += PlayerStart;
            _levelStatus.OnCompleteLevel += PlayerEnd;
            _levelStatus.OnFailedLevel += PlayerEnd;
            
            _shelfsOnLevel = new List<IShelf>(FindObjectsOfType<ShelfBlock>());
            _totalBookCount = ((BooksLevelData) _levelSystem.CurrentLevel).BooksOnLevel;

            StartCoroutine(WaitPlaceAllBooks());

            var color = _levelSystem.CurrentLevel.BackgroundColor;
            color.a = 0.5f;
            var colorStr = $"#{ColorUtility.ToHtmlStringRGBA(color)}";
            
            _camera.backgroundColor = color;

#if !UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IOS
            UpdateBodyColor(colorStr);
#endif
        }

        private void PlayerStart(object sender, EventArgs e)
        {
            _inputable.OnStartInput+= OnStartInput;
            _inputable.OnInput += OnInput;
            _inputable.OnEndInput += OnEndInput;

            _camera.DOOrthoSize(_defaultFOV, 0.25f);
        }
        private void PlayerEnd(object sender, EventArgs e)
        {
            _inputable.OnStartInput -= OnStartInput;
            _inputable.OnInput -= OnInput;
            _inputable.OnEndInput -= OnEndInput;
        }

        private void OnStartInput(object sender, ShelfBookArgs e)
        {
            if(_movableObject != null) return;
            
            _movableObject = e.Movable;
            _movableObject.OnStartMoving(e.Shelf);
        }
        private void OnInput(object sender, Vector3 e)
        {
            if (_movableObject != null)
            {
                var globalPosition = _camera.ScreenToWorldPoint(e);
                _movableObject.OnMoving(globalPosition);
            }
        }
        private void OnEndInput(object sender, EventArgs e)
        {
            if (_movableObject == null) return;
            
            _movableObject.OnEndMoving();
            _movableObject = null;
        }
        
        
        private IEnumerator WaitPlaceAllBooks()
        {
            IShelf shelf = null;
            
            yield return new WaitUntil(() =>
                {
                    shelf = _shelfsOnLevel.Find(s => s.BooksOnShelf == _totalBookCount);
                    return shelf != null;
                }
            );

            if (shelf is ShelfBlock block)
            {
                DOTween.Sequence()
                    .Append(_camera.transform.DOLookAt(block.transform.position + 2f * Vector3.up, 1f))
                    .Join(_camera.DOOrthoSize(_loopFOV, 1f));
            }
            
            _levelStatus.CompleteLevel();
        }
        
    }
}