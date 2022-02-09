using System.Collections.Generic;
using BookLogic;
using UnityEngine;

namespace ATG.LevelControl
{
    public class ShelfBlock: EnvironmentBlock
    {
        private SortedSet<Book> _booksOnShelf;

        public void InitBooks(Book[] books)
        {
            _booksOnShelf = new SortedSet<Book>(books,new SortBySize());

            foreach (var book in _booksOnShelf)
            {
                Debug.Log(book.name);
            }
        }
    }
}