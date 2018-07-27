using System.Collections.Generic;
using eLibrary.Model;

namespace eLibrary.Service
{
    public interface IBookDataService
    {
        void AddNewBook(BookData bookData);
        void DeleteBookData(int id);
        void EditBook(BookData bookData);
        BookData GetBookData(int bookID);
        List<BookData> GetBookDataWithConditions(SearchArg arg);
        string GetCurrentBookStatus(int id);
    }
}