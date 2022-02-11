using BookLogic;

namespace ATG.LevelControl
{
    public interface IShelf
    {
        int BooksOnShelf { get; }
        
        bool TryAddBook(Book book);
        Book RemoveBook();
    }
}