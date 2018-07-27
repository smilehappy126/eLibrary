using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eLibrary.Service
{
    public class BookClassService : IBookClassService
    {
        private Dao.BookClassDao bookClassDao { get; set; }

        // 以List的格式去取得所有的BOOK_CLASS
        public List<SelectListItem> GetBookClassList()
        {
            Dao.IBookClassDao bookClassDao = new Dao.BookClassDao();
            return bookClassDao.GetBookClassList();
        }
    }
}