using System;
using System.Collections.Generic;
using System.Linq;
using BookLogic;
using UnityEngine;

namespace ATG.LevelControl
{
    public class ShelfBlock: EnvironmentBlock, IShelf
    {
        [Space(5)]
        [SerializeField] private Transform _spawnPosition;
        
        private SortedSet<Book> _booksOnShelf;

        private Vector3 _placePosition;
        
        public void InitShelf(Book[] books)
        {
           InitShelf();
           
            if (books.Length > 0)
            {
                _booksOnShelf = new SortedSet<Book>(new SortBySize());
                SpawnBooks(books);
            }
        }
        public void InitShelf()
        {
            _placePosition = _spawnPosition.position;
        }
        
        public bool TryAddBook(Book book)
        {
            if (!_booksOnShelf.Contains(book))
            {
                if (_booksOnShelf.Count > 0)
                {
                    var lastBook = _booksOnShelf.Last();

                    var compare = new SortBySize().Compare(book, lastBook);

                    bool isCompare = compare == 1;

                    if (isCompare)
                    {
                        AddNewBook();
                    }
                    
                    return isCompare;
                }
                else
                {
                    AddNewBook();
                    return true;
                }

                void AddNewBook()
                {
                    _booksOnShelf.Add(book);
                    AddBook(book);
                }
            }
            
            return false;
        }

        public Book RemoveBook()
        {
            if (_booksOnShelf.Count > 0)
            {
                var selected = _booksOnShelf.Last();
                selected.transform.SetParent(null);

                _booksOnShelf.Remove(selected);

                _placePosition -= selected.Thickness / 2f * Vector3.up;
                
                return selected;
            }

            throw new NullReferenceException("Cant find book on this shelf");
        }
        
        private void AddBook(Book book)
        {
            var bookTransform= book.transform;
            
            bookTransform.position = _placePosition + book.Thickness / 4f * Vector3.up;
            bookTransform.SetParent(transform);
            
            _placePosition = bookTransform.position + book.Thickness / 4f * Vector3.up;
        }
        private void SpawnBooks(Book[] blockPrefabs)
        {
            foreach (var b in blockPrefabs)
            {
                var spawnedBook = Instantiate(b);
                _booksOnShelf.Add(spawnedBook);
            }
            
            foreach (var book in _booksOnShelf)
            {
                AddBook(book);
            }
        }
    }
}