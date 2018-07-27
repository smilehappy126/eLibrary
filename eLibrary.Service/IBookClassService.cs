using System.Collections.Generic;
using System.Web.Mvc;

namespace eLibrary.Service
{
    public interface IBookClassService
    {
        List<SelectListItem> GetBookClassList();
    }
}