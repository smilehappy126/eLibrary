using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eLibrary.Service
{
    public class BookCodeService : IBookCodeService
    {
        private Dao.BookCodeDao bookCodeDao { get; set; }

        // 取得CodeList
        public List<SelectListItem> GetCodeList()
        {
            Dao.IBookCodeDao bookCodeDao = new Dao.BookCodeDao();
            return bookCodeDao.GetCodeList();
        }
    }
}