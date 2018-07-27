using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace eLibrary.Models
{
    public class BookDataService
    {
        /// 取得DB連線字串
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnect"].ConnectionString.ToString();
        }

        /// 搜尋輸入的條件取得Book Data
        public List<Models.BookData> GetBookDataWithConditions(Models.SearchArg arg)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"SELECT       bd.BOOK_ID AS BOOK_ID, bd.BOOK_NAME AS BOOK_NAME, bcl.BOOK_CLASS_NAME AS BOOK_CLASS_ID, 
                                            bd.BOOK_AUTHOR AS BOOK_AUTHOR, bd.BOOK_BOUGHT_DATE AS BOOK_BOUGHT_DATE, bd.BOOK_PUBLISHER AS BOOK_PUBLISHER, 
                                            bd.BOOK_NOTE AS BOOK_NOTE, bco.CODE_NAME AS BOOK_STATUS, m.USER_ENAME+'('+m.USER_CNAME+')' AS BOOK_KEEPER,
                                            bd.CREATE_DATE AS CREATE_DATE, bd.CREATE_USER AS CREATE_USER, bd.MODIFY_DATE AS MODIFY_DATE,
                                            bd.MODIFY_USER AS MODIFY_USER
                               FROM         BOOK_DATA AS bd 
                               INNER JOIN   BOOK_CLASS AS bcl ON bd.BOOK_CLASS_ID = bcl.BOOK_CLASS_ID
                               INNER JOIN　 BOOK_CODE AS bco  ON bd.BOOK_STATUS = bco.CODE_ID
                               LEFT  JOIN   MEMBER_M AS m   ON  bd.BOOK_KEEPER = m.USER_ID
                               WHERE       ( UPPER(bd.BOOK_NAME) LIKE UPPER(@bookName) or @bookName='')   AND
                                           (bd.BOOK_CLASS_ID = @bookClass or @bookClass='')             AND
                                           (bd.BOOK_KEEPER = @bookKeeper or @bookKeeper='')             AND
                                           (bd.BOOK_STATUS = @bookStatus or @bookStatus='')
                               ORDER BY    CREATE_DATE DESC
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookName", arg.BOOK_NAME == null ? string.Empty : "%"+arg.BOOK_NAME+"%"));
                cmd.Parameters.Add(new SqlParameter("@bookClass", arg.BOOK_CLASS_ID == null ? string.Empty : arg.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@bookKeeper", arg.BOOK_KEEPER == null ? string.Empty : arg.BOOK_KEEPER));
                cmd.Parameters.Add(new SqlParameter("@bookStatus", arg.BOOK_STATUS == null ? string.Empty : arg.BOOK_STATUS));
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                conn.Close();
                return MapBookDataToList(dt);
            }
        }
        
        /// 新增書籍
        public void AddNewBook(Models.BookData bookData)
        {
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"
                               BEGIN TRY
                                    BEGIN TRANSACTION
                                       INSERT INTO    BOOK_DATA(BOOK_NAME, BOOK_CLASS_ID, BOOK_AUTHOR, BOOK_BOUGHT_DATE, BOOK_PUBLISHER, BOOK_STATUS, BOOK_KEEPER, BOOK_NOTE, CREATE_DATE, CREATE_USER, MODIFY_DATE, MODIFY_USER) 
                                       VALUES                  (@bookName, @bookClassID,  @bookAuthor, @bookBoughtDate,  @bookPublisher, @bookStatus, @bookKeeper, @bookNote, CURRENT_TIMESTAMP, 'RU', CURRENT_TIMESTAMP, 'RU' )
                                    COMMIT TRANSACTION
                               END TRY
                               BEGIN CATCH
                                    ROlLBACK TRANSACTION
                               END CATCH;
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookName", bookData.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@bookAuthor", bookData.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@bookPublisher", bookData.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@bookNote", bookData.BOOK_NOTE));
                cmd.Parameters.Add(new SqlParameter("@bookBoughtDate", bookData.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@bookClassID", bookData.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@bookStatus", "A"));
                cmd.Parameters.Add(new SqlParameter("@bookKeeper", string.Empty));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /// 根據BOOK_ID去取得特定的一本書
        public Models.BookData GetBookData(int bookID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"SELECT       bd.BOOK_ID AS BOOK_ID, bd.BOOK_NAME AS BOOK_NAME, bd.BOOK_CLASS_ID AS BOOK_CLASS_ID,
                                            bcl.BOOK_CLASS_NAME AS BOOK_CLASS_NAME, bd.BOOK_AUTHOR AS BOOK_AUTHOR, bd.BOOK_BOUGHT_DATE AS BOOK_BOUGHT_DATE,
                                            bd.BOOK_PUBLISHER AS BOOK_PUBLISHER, bd.BOOK_NOTE AS BOOK_NOTE, bd.BOOK_STATUS AS BOOK_STATUS,
                                            bco.CODE_NAME AS BOOK_STATUS_NAME, bd.BOOK_KEEPER AS BOOK_KEEPER, m.USER_ENAME+'('+m.USER_CNAME+')' AS BOOK_KEEPER_NAME, 
                                            bd.CREATE_DATE AS CREATE_DATE, bd.CREATE_USER AS CREATE_USER, bd.MODIFY_DATE AS MODIFY_DATE, 
                                            bd.MODIFY_USER AS MODIFY_USER
                               FROM         BOOK_DATA AS bd 
                               INNER JOIN   BOOK_CLASS AS bcl ON bd.BOOK_CLASS_ID = bcl.BOOK_CLASS_ID
                               INNER JOIN　 BOOK_CODE AS bco  ON bd.BOOK_STATUS = bco.CODE_ID
                               LEFT  JOIN   MEMBER_M AS m   ON  bd.BOOK_KEEPER = m.USER_ID
                               WHERE        bd.BOOK_ID = @bookID
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookID", bookID));
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                conn.Close();
                Models.BookData bookData = new Models.BookData();
                foreach (DataRow row in dt.Rows)
                {
                    bookData.BOOK_ID = (int)row["BOOK_ID"];
                    bookData.BOOK_NAME = row["BOOK_NAME"].ToString() == null ? string.Empty : row["BOOK_NAME"].ToString();
                    bookData.BOOK_CLASS_ID = row["BOOK_CLASS_ID"].ToString();
                    bookData.BOOK_CLASS_NAME = row["BOOK_CLASS_NAME"].ToString();
                    bookData.BOOK_AUTHOR = row["BOOK_AUTHOR"].ToString() == null? string.Empty : row["BOOK_AUTHOR"].ToString();
                    bookData.BOOK_BOUGHT_DATE = Convert.ToDateTime(row["BOOK_BOUGHT_DATE"]).ToString("yyyy/MM/dd HH:mm:ss");
                    bookData.BOOK_PUBLISHER = row["BOOK_PUBLISHER"].ToString() == null ? string.Empty : row["BOOK_PUBLISHER"].ToString();
                    bookData.BOOK_NOTE = row["BOOK_NOTE"].ToString() == null ? string.Empty : row["BOOK_NOTE"].ToString();
                    bookData.BOOK_STATUS = row["BOOK_STATUS"].ToString();
                    bookData.BOOK_STATUS_NAME = row["BOOK_STATUS_NAME"].ToString();
                    bookData.BOOK_KEEPER = row["BOOK_KEEPER"].ToString() == null ? string.Empty : row["BOOK_KEEPER"].ToString();
                    bookData.BOOK_KEEPER_NAME = row["BOOK_KEEPER_NAME"].ToString() == null ? string.Empty : row["BOOK_KEEPER_NAME"].ToString();
                }
                return  bookData;
            }
        }
    
        /// 根據輸入的BOOK_ID去找書，編輯該書本
        public void EditBook(Models.BookData bookData)
        {
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"
                               BEGIN TRY
                                    BEGIN TRANSACTION
                                       UPDATE         bd
                                       SET            bd.BOOK_NAME = @bookName, bd.BOOK_CLASS_ID = @bookClassID, bd.BOOK_AUTHOR = @bookAuthor,
                                                      bd.BOOK_BOUGHT_DATE = @bookBoughtDate, bd.BOOK_PUBLISHER = @bookPublisher,bd.BOOK_STATUS = @bookStatus, 
                                                      bd.BOOK_KEEPER = @bookKeeper, bd.BOOK_NOTE = @bookNote, bd.MODIFY_DATE = CURRENT_TIMESTAMP,
                                                      bd.MODIFY_USER = 'RU'
                                       FROM           BOOK_DATA AS bd
                                       WHERE          bd.BOOK_ID = @bookID
                                    COMMIT TRANSACTION
                               END TRY
                               BEGIN CATCH
                                    ROlLBACK TRANSACTION
                               END CATCH;
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookID", bookData.BOOK_ID));
                cmd.Parameters.Add(new SqlParameter("@bookName", bookData.BOOK_NAME == null ? string.Empty : bookData.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@bookClassID", bookData.BOOK_CLASS_ID == null ? string.Empty : bookData.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@bookAuthor", bookData.BOOK_AUTHOR == null ? string.Empty : bookData.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@bookKeeper", bookData.BOOK_KEEPER == null ? string.Empty : bookData.BOOK_KEEPER));
                cmd.Parameters.Add(new SqlParameter("@bookStatus", bookData.BOOK_STATUS == null ? string.Empty : bookData.BOOK_STATUS));
                cmd.Parameters.Add(new SqlParameter("@bookBoughtDate", bookData.BOOK_BOUGHT_DATE == null ? string.Empty : bookData.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@bookPublisher", bookData.BOOK_PUBLISHER == null ? string.Empty : bookData.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@bookNote", bookData.BOOK_NOTE == null ? string.Empty : bookData.BOOK_NOTE));
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /// 根據特定的BOOK_ID回傳這本書的目前借用狀況
        public string GetCurrentBookStatus(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"
                               SELECT       bd.BOOK_STATUS AS BOOK_STATUS 
                               FROM         BOOK_DATA AS bd 
                               WHERE        bd.BOOK_ID = @bookID
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookID", id));
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                conn.Close();
            }
            String currentStatus = dt.Rows[0].ItemArray[0].ToString();
            return currentStatus;
        }

        /// 找到特定BOOK_ID的書並且刪除該本書
        public void DeleteBookData(int id)
        {
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                string sql = @"
                               BEGIN TRY
                                    BEGIN TRANSACTION
                                       DELETE FROM   BOOK_DATA  WHERE  BOOK_DATA.BOOK_ID = @bookID
                                    COMMIT TRANSACTION
                               END TRY
                               BEGIN CATCH
                                    ROlLBACK TRANSACTION
                               END CATCH;
                                ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@bookID", id));
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }



        /// Map資料進List
        private List<Models.BookData> MapBookDataToList(DataTable bookData)
        {
            List<Models.BookData> bookDatas = new List<BookData>();

            ///// 創建CODE_ID跟CODE_NAME的對照表
            //Dictionary<string, string> codeMap = new Dictionary<string, string>();
            ///// 將BOOK_CODE中的CODE_ID跟CODE_NAME對照放入Dictionary中
            //foreach (DataRow row in bookData.Tables[1].Rows)
            //{
            //    codeMap.Add(row["CODE_ID"].ToString(), row["CODE_NAME"].ToString());
            //}

            ///// 創建USER_ID跟USERNAME的對照表
            //Dictionary<string, string> nameMap = new Dictionary<string, string>();
            ///// 將BOOK_DATA中的BOOK_KEEPER跟USER_NAME對照放入Dictionary中
            //foreach (DataRow row in bookData.Tables[2].Rows)
            //{
            //    /// 將使用者的名字組合成 USER_ENAME(USER_CNAME)
            //    string nameSet = row["USER_ENAME"].ToString() + "("+ row["USER_CNAME"].ToString()+")";
            //    nameMap.Add(row["USER_ID"].ToString(), nameSet);
            //}

            ///// 創建BOOK_CLASS_ID跟BOOK_CLASS_NAME的對照表
            //Dictionary<string, string> classMap = new Dictionary<string, string>();
            ///// 將BOOK_CLASS中的BOOK_CLASS_ID跟BOOK_CLASS_NAME對照放入Dictionary中
            //foreach (DataRow row in bookData.Tables[3].Rows)
            //{
            //    classMap.Add(row["BOOK_CLASS_ID"].ToString(), row["BOOK_CLASS_NAME"].ToString());
            //}

            /// 批次將BOOK_DATA資料表中的資料撈出來放入List<Models.BOOK_DATA>中
            foreach (DataRow row in bookData.Rows)
            {
                bookDatas.Add(new BookData()
                {
                    BOOK_ID = (int)row["BOOK_ID"],
                    BOOK_NAME = row["BOOK_NAME"].ToString() == "" ? "" : row["BOOK_NAME"].ToString(),
                    BOOK_CLASS_ID = row["BOOK_CLASS_ID"].ToString(),
                    BOOK_AUTHOR = row["BOOK_AUTHOR"].ToString() == "" ? "" : row["BOOK_AUTHOR"].ToString(),
                    BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString(),
                    BOOK_PUBLISHER = row["BOOK_PUBLISHER"].ToString() == "" ? "" : row["BOOK_PUBLISHER"].ToString(),
                    BOOK_NOTE = row["BOOK_NOTE"].ToString() == "" ? "" : row["BOOK_NOTE"].ToString(),
                    BOOK_STATUS = row["BOOK_STATUS"].ToString(),
                    BOOK_KEEPER = row["BOOK_KEEPER"].ToString() == "" ? "無資料" : row["BOOK_KEEPER"].ToString(),
                    CREATE_DATE = Convert.ToDateTime(row["CREATE_DATE"]),
                    CREATE_USER = row["CREATE_USER"].ToString(),
                    MODIFY_DATE = Convert.ToDateTime(row["MODIFY_DATE"]),
                    MODIFY_USER = row["MODIFY_USER"].ToString()
                });
            }
            return bookDatas;
        }
    }
}