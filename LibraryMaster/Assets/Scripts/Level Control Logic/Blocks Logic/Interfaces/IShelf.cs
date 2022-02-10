using BookLogic;

namespace ATG.LevelControl
{
    public interface IShelf
    {
        void AddBook(Book book);
        Book RemoveBook();
    }
}