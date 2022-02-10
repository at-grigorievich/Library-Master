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

        public void AddBook(Book book)
        {
            var bookTransform= book.transform;
            
            bookTransform.position = _placePosition + book.Thickness / 4f * Vector3.up;
            bookTransform.SetParent(transform);
            
            _placePosition = bookTransform.position + book.Thickness / 4f * Vector3.up;
        }
        public Book RemoveBook()
        {
            return _booksOnShelf.Last();
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