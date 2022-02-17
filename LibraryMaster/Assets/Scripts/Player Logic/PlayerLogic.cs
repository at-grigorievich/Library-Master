using System;
using System.Collections;
using System.Collections.Generic;
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

            _camera.backgroundColor = _levelSystem.CurrentLevel.BackgroundColor;
        }

        private void PlayerStart(object sender, EventArgs e)
        {
            _inputable.OnStartTouch += OnStartTouch;
            _inputable.OnTouching += OnTouching;
            _inputable.OnEndTouch += OnEndTouch;

            _camera.DOOrthoSize(_defaultFOV, 0.25f);
        }
        private void PlayerEnd(object sender, EventArgs e)
        {
            _inputable.OnStartTouch -= OnStartTouch;
            _inputable.OnTouching -= OnTouching;
            _inputable.OnEndTouch -= OnEndTouch;
        }

        private void OnStartTouch(object sender, ShelfBookArgs e)
        {
            _movableObject = e.Movable;
            _movableObject.OnStartMoving(e.Shelf);
        }
        private void OnEndTouch(object sender, EventArgs e)
        {
            if (_movableObject != null)
            {
                _movableObject.OnEndMoving();
                _movableObject = null;
            }
        }
        private void OnTouching(object sender, Vector3 e)
        {
            if (_movableObject != null)
            {
                Vector3 globalPosition = _camera.ScreenToWorldPoint(e);
                _movableObject.OnMoving(globalPosition);
            }
        }

        private IEnumerator WaitPlaceAllBooks()
        {
            IShelf _shelf = null;
            
            yield return new WaitUntil(() =>
                {
                    _shelf = _shelfsOnLevel.Find(s => s.BooksOnShelf == _totalBookCount);
                    return _shelf != null;
                }
            );

            if (_shelf is ShelfBlock block)
            {
                DOTween.Sequence()
                    .Append(_camera.transform.DOLookAt(block.transform.position + 2f * Vector3.up, 1f))
                    .Join(_camera.DOOrthoSize(_loopFOV, 1f));
            }
            
            _levelStatus.CompleteLevel();
        }
        
    }
}