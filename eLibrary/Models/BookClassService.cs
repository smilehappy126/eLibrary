using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eLibrary.Models
{
    public class BookClassService
    {
        /// 取得DB連線字串
        private string GetDBConnectionString()
        {
            return  System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnect"].ConnectionString.ToString();
        }

        /// 取得所有BookClass
        //public List <Models.BookClass> GetAllBookClass()
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
        //    {
        //        conn.Open();
        //        string sql = @"SELECT * FROM BOOK_CLASS ";
        //        SqlCommand cmd = new SqlCommand(sql,conn);
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        dataAdapter.Fill(ds);
        //        conn.Close();
        //        return MapBookClassDataToList(ds);
        //    }
        //}

        // 以List的格式去取得所有的BOOK_CLASS
        public List<SelectListItem> GetBookClassList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"SELECT      bc.BOOK_CLASS_ID AS BOOK_CLASS_ID, bc.BOOK_CLASS_NAME AS BOOK_CLASS_NAME
                               FROM        BOOK_CLASS AS bc
                               GROUP BY    bc.BOOK_CLASS_ID, bc.BOOK_CLASS_NAME
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                conn.Close();
                return MapBookClassToList(dt);
            }
        }


        /// Map資料進List
        //private List<Models.BookClass> MapBookClassDataToList(DataSet bookClass)
        //{
        //    List<Models.BookClass> result = new List<BookClass>();
        //    foreach (DataRow row in bookClass.Tables[0].Rows)
        //    {
        //        result.Add(new BookClass()
        //        {
        //            BOOK_CLASS_ID =row["BOOK_CLASS_ID"].ToString(),
        //            BOOK_CLASS_NAME = row["BOOK_CLASS_NAME"].ToString()
        //        });
        //    }
        //    return result;
        //}

        /// 以List的格式獲得BOOK_CLASS_ID 跟 BOOK_CLASS_Name
        private List<SelectListItem> MapBookClassToList(DataTable bookClass)
        {
            List<SelectListItem> bookClassList = new List<SelectListItem>();
            foreach (DataRow row in bookClass.Rows)
            {
                bookClassList.Add(new SelectListItem()
                {
                    Text = row["BOOK_CLASS_NAME"].ToString(),
                    Value = row["BOOK_CLASS_ID"].ToString()
                });
                
            }
            return bookClassList;
        }
    }
}