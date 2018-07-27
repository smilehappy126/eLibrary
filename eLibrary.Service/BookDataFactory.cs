using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLibrary.Dao;

namespace eLibrary.Service                  
{
    public class BookDataFactory
    {
        public IBookDataDao AddNewBook()
        {
            IBookDataDao result;
            switch (Common.ConfigTool.GetAppSetting("AddNewBook"))
            {
                case "bookDataDao":
                    result = new BookDataDao();
                    break;
                default:
                    result = new BookDataDao();
                    break;

            }
            return result;
        }

        public IBookDataDao GetBookData()
        {
            IBookDataDao result;
            switch (Common.ConfigTool.GetAppSetting("GetBookData"))
            {
                case "bookDataDao":
                    result = new BookDataDao();
                    break;
                default:
                    result = new BookDataDao();
                    break;

            }
            return result;
        }

        public IBookDataDao EditBook()
        {
            IBookDataDao result;
            switch (Common.ConfigTool.GetAppSetting("GetBookData"))
            {
                case "bookDataDao":
                    result = new BookDataDao();
                    break;
                default:
                    result = new BookDataDao();
                    break;

            }
            return result;
        }
    }
}
