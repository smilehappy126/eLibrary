using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eLibrary.Service
{
    public class MemberService : IMemberService
    {
        private Dao.MemberDao memberDao { get; set; }

        // 取得MemberList
        public List<SelectListItem> GetMemberList()
        {
            Dao.IMemberDao memberDao = new Dao.MemberDao();
            return memberDao.GetMemberList();
        }
    }
}