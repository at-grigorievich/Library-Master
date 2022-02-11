using System;
using System.Collections;
using System.Collections.Generic;
using ATG.LevelControl;
using BookLogic;
using PlayerLogic;
using UnityEngine;
using Zenject;

namespace Player_Logic
{
    public class PlayerLogic : MonoBehaviour
    {
        [Inject] private IInputable _inputable;
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
            _inputable.OnStartTouch += OnStartTouch;
            _inputable.OnTouching += OnTouching;
            _inputable.OnEndTouch += OnEndTouch;

            _shelfsOnLevel = new List<IShelf>(FindObjectsOfType<ShelfBlock>());
            _totalBookCount = ((BooksLevelData) _levelSystem.CurrentLevel).BooksOnLevel;

            StartCoroutine(WaitPlaceAllBooks());
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
            yield return new WaitUntil(() =>
                _shelfsOnLevel.Find(s => s.BooksOnShelf == _totalBookCount) != null
            );
            Debug.Log("finish");
        }
        
    }
}