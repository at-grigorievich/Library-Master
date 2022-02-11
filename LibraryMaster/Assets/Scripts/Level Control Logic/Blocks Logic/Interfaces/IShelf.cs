using BookLogic;

namespace ATG.LevelControl
{
    public interface IShelf
    {
        bool TryAddBook(Book book);
        Book RemoveBook();
    }
}