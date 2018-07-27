using System.Collections.Generic;
using eLibrary.Model;

namespace eLibrary.Dao
{
    public interface IBookDataDao
    {
        void AddNewBook(BookData bookData);
        void DeleteBookData(int id);
        void EditBook(BookData bookData);
        BookData GetBookData(int bookID);
        List<BookData> GetBookDataWithConditions(SearchArg arg);
        string GetCurrentBookStatus(int id);
    }
}