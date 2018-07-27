using System.Collections.Generic;
using System.Web.Mvc;

namespace eLibrary.Dao
{
    public interface IBookClassDao
    {
        List<SelectListItem> GetBookClassList();
    }
}