using System.Collections.Generic;
using System.Web.Mvc;

namespace eLibrary.Service
{
    public interface IBookCodeService
    {
        List<SelectListItem> GetCodeList();
    }
}