using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eLibrary.Model
{
    public class BookData
    {
        [DisplayName("流水號")]
        [Required(ErrorMessage = "此欄位必填")]
        public int BOOK_ID { get; set; }

        [DisplayName("書籍名稱")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_NAME { get; set; }

        [DisplayName("類別代號")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_CLASS_ID { get; set; }

        [DisplayName("類別名稱")]
        public string BOOK_CLASS_NAME { get; set; }

        [DisplayName("書籍作者")]
        public string BOOK_AUTHOR { get; set; }

        [DisplayName("書籍購書日期")]
        public string BOOK_BOUGHT_DATE { get; set; }

        [DisplayName("出版商")]
        public string BOOK_PUBLISHER { get; set; }

        [DisplayName("內容簡介")]
        [AllowHtml()]
        public string BOOK_NOTE { get; set; }

        [DisplayName("狀態")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BOOK_STATUS { get; set; }

        [DisplayName("狀態名稱")]
        public string BOOK_STATUS_NAME { get; set; }

        [DisplayName("書籍保管人")]
        public string BOOK_KEEPER { get; set; }

        [DisplayName("書籍保管人名稱")]
        public string BOOK_KEEPER_NAME { get; set; }

        [DisplayName("建立時間")]
        public DateTime CREATE_DATE { get; set; }

        [DisplayName("建立使用者")]
        public string CREATE_USER { get; set; }

        [DisplayName("修改時間")]
        public DateTime MODIFY_DATE { get; set; }

        [DisplayName("修改使用者")]
        public string MODIFY_USER { get; set; }

    }
}