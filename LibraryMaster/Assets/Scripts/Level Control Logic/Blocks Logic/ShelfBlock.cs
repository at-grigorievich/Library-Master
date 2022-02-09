using BookLogic;
using UnityEngine;

namespace ATG.LevelControl
{
    public class ShelfBlock: EnvironmentBlock
    {
        public void InitBooks(Book[] books)
        {
            Debug.Log(books.Length);
        }
    }
}