using System.Collections.Generic;
using System.Web.Mvc;

namespace eLibrary.Dao
{
    public interface IBookCodeDao
    {
        List<SelectListItem> GetCodeList();
    }
}