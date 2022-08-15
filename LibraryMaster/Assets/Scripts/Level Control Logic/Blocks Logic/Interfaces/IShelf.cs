using BookLogic;

namespace ATG.LevelControl
{
    public interface IShelf
    {
        
        bool ContainsBook { get; }
        int BooksOnShelf { get; }
        
        bool TryAddBook(Book book);
        Book RemoveBook();
    }
}