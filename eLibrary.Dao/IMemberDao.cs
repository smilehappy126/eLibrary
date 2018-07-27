using System.Collections.Generic;
using System.Web.Mvc;

namespace eLibrary.Dao
{
    public interface IMemberDao
    {
        List<SelectListItem> GetMemberList();
    }
}