using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eLibrary.Service
{
    public class BookDataService : IBookDataService
    {
        private Dao.BookDataDao bookDataDao { get; set; }

        /// 搜尋輸入的條件取得Book Data
        public List<Model.BookData> GetBookDataWithConditions(Model.SearchArg arg)
        {
            Dao.IBookDataDao bookDataDao = new Dao.BookDataDao();
            return bookDataDao.GetBookDataWithConditions(arg);
        }   
        
        /// 新增書籍
        public void AddNewBook(Model.BookData bookData)
        {
            Dao.IBookDataDao bookDataDao = new Dao.BookDataDao();
            bookDataDao.AddNewBook(bookData);
        }

        /// 根據BOOK_ID去取得特定的一本書
        public Model.BookData GetBookData(int bookID)
        {
            Dao.IBookDataDao bookDataDao = new Dao.BookDataDao();
            return bookDataDao.GetBookData(bookID);
        }
    
        /// 根據輸入的BOOK_ID去找書，編輯該書本
        public void EditBook(Model.BookData bookData)
        {
            Dao.IBookDataDao bookDataDao = new Dao.BookDataDao();
            bookDataDao.EditBook(bookData);
        }

        /// 根據特定的BOOK_ID回傳這本書的目前借用狀況
        public string GetCurrentBookStatus(int id)
        {
            Dao.IBookDataDao bookDataDao = new Dao.BookDataDao();
            return bookDataDao.GetCurrentBookStatus(id);
        }

        /// 找到特定BOOK_ID的書並且刪除該本書
        public void DeleteBookData(int id)
        {
            Dao.IBookDataDao bookDataDao = new Dao.BookDataDao();
            bookDataDao.DeleteBookData(id);
        }
    }
}