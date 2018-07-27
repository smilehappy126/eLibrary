using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eLibrary.Model
{
    public class BookCode
    {
        [DisplayName("CodeType")]
        [Required(ErrorMessage = "此欄位必填")]
        public string CODE_TYPE { get; set; }

        [DisplayName("CodeId")]
        [Required(ErrorMessage = "此欄位必填")]
        public string CODE_ID { get; set; }

        [DisplayName("CodeType描述")]
        public string CODE_TYPE_DESC { get; set; }

        [DisplayName("CodeId描述")]
        public string CODE_NAME { get; set; }

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