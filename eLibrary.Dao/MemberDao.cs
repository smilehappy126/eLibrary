using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eLibrary.Dao
{
    public class MemberDao : IMemberDao
    {
        /// 取得DB連線字串
        public string GetDBConnectionString()
        {
            return eLibrary.Common.ConfigTool.GetDBConnectionString("sqlConnect");
        }

        /// 取得所有Member
        //public List<Models.Member> GetAllMember()
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
        //    {
        //        conn.Open();
        //        string sql = @"SELECT * FROM MEMBER_M ";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        dataAdapter.Fill(ds);
        //        conn.Close();
        //        return MapMemberDataToList(ds);
        //    }
        //}

        // 取得MemberList
        public List<SelectListItem> GetMemberList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"SELECT      m.USER_ID AS USER_ID, m.USER_ENAME AS USER_ENAME, m.USER_CNAME AS USER_CNAME
                               FROM        MEMBER_M AS m
                               GROUP BY    USER_ID, USER_ENAME, USER_CNAME
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                conn.Close();
                return MapMemberToList(dt);
            }
        }

        /// 獲得USER_ID跟 USER_CName, USER_ENAME
        private List<SelectListItem> MapMemberToList(DataTable member)
        {
            List<SelectListItem> memberList = new List<SelectListItem>();
            foreach (DataRow row in member.Rows)
            {
                memberList.Add(new SelectListItem()
                {
                    Text = row["USER_ENAME"].ToString()+"("+ row["USER_CNAME"].ToString()+")",
                    Value = row["USER_ID"].ToString()
                });

            }
            return memberList;
        }

        /// Map資料進List
        //private List<Models.Member> MapMemberDataToList(DataSet member)
        //{
        //    List<Models.Member> result = new List<Member>();
        //    foreach (DataRow row in member.Tables[0].Rows)
        //    {
        //        result.Add(new Member()
        //        {
        //            USER_ID = row["USER_ID"].ToString(),
        //            USER_CNAME = row["USER_CNAME"].ToString(),
        //            USER_ENAME = row["USER_ENAME"].ToString(),
        //            CREATE_DATE = Convert.ToDateTime(row["CREATE_DATE"]),
        //            MODIFY_DATE = Convert.ToDateTime(row["MODIFY_DATE"])
        //        });
        //    }
        //    return result;
        //}
    }
}