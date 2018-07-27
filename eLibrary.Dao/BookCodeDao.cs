using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eLibrary.Dao
{
    public class BookCodeDao : IBookCodeDao
    {
        /// 取得DB連線字串
        public string GetDBConnectionString()
        {
            return eLibrary.Common.ConfigTool.GetDBConnectionString("sqlConnect");
        }

        /// 取得所有Code
        //public List<Models.BookCode> GetAllCode()
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
        //    {
        //        conn.Open();
        //        string sql = @"SELECT * FROM BOOK_CODE ";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        dataAdapter.Fill(ds);
        //        conn.Close();
        //        return MapBookCodeDataToList(ds);
        //    }
        //}

        // 取得CodeList
        public List<SelectListItem> GetCodeList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"SELECT      bc.CODE_ID AS CODE_ID, bc.CODE_NAME AS CODE_NAME
                               FROM        BOOK_CODE AS bc
                               GROUP BY    CODE_ID, CODE_NAME
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                conn.Close();
                return MapCodeToList(dt);
            }
        }

        /// 獲得USER_ID跟 USER_CName, USER_ENAME
        private List<SelectListItem> MapCodeToList(DataTable bookCode)
        {
            List<SelectListItem> codeList = new List<SelectListItem>();
            foreach (DataRow row in bookCode.Rows)
            {
                codeList.Add(new SelectListItem()
                {
                    Text = row["CODE_NAME"].ToString(),
                    Value = row["CODE_ID"].ToString()
                });

            }
            return codeList;
        }


        /// Map資料進List
        //private List<Models.BookCode> MapBookCodeDataToList(DataSet bookCode)
        //{
        //    List<Models.BookCode> result = new List<BookCode>();
        //    foreach (DataRow row in bookCode.Tables[0].Rows)
        //    {
        //        result.Add(new BookCode()
        //        {
        //            CODE_ID = row["CODE_ID"].ToString(),
        //            CODE_NAME = row["CODE_NAME"].ToString(),
        //            CREATE_DATE = Convert.ToDateTime(row["CREATE_DATE"]),
        //            CREATE_USER = row["CREATE_USER"].ToString(),
        //            MODIFY_DATE = Convert.ToDateTime(row["MODIFY_DATE"]),
        //            MODIFY_USER = row["MODIFY_USER"].ToString()
        //        });
        //    }
        //    return result;
        //}
    }
}