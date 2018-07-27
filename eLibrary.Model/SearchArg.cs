using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eLibrary.Model
{
    public class SearchArg
    {
        public string BOOK_NAME { get; set; }

        public string BOOK_CLASS_ID { get; set; }

        public string BOOK_KEEPER{ get; set; }

        public string BOOK_STATUS { get; set; }
        
    }
}