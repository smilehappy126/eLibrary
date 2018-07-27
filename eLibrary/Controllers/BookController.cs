using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLibrary.Model;
using eLibrary.Service;

namespace eLibrary.Controllers
{
    public class BookController : Controller
    {
        private IBookDataService bookDataService {get; set; }
        private IBookClassService bookClassService { get; set; }
        private IBookCodeService bookCodeService { get; set; }
        private IMemberService memberService { get; set; }

        // 顯示首頁
        public ActionResult Index()
        {
            return View();
        }

        // 取得所有的BOOK_CLASS
        public JsonResult GetAllBookClassList()
        {
            Service.BookClassService bookClassService = new Service.BookClassService();
            return Json(bookClassService.GetBookClassList(), JsonRequestBehavior.AllowGet);

        }

        //  取得所有的BOOK_KEEPER
        public JsonResult GetAllBookKeeperList()
        {
            Service.MemberService memberService = new Service.MemberService();
            return Json(memberService.GetMemberList(), JsonRequestBehavior.AllowGet);

        }

        //  取得所有的BOOK_STATUS
        public JsonResult GetAllBookStatusList()
        {
            Service.BookCodeService bookCodeService = new Service.BookCodeService();
            return Json(bookCodeService.GetCodeList(), JsonRequestBehavior.AllowGet);

        }

        // Index頁面的刪除
        public ActionResult IndexDeleteBookData(int id)
        {
            Service.BookDataService bookDataService = new Service.BookDataService();
            bookDataService.DeleteBookData(id);
            return Redirect("/");
        }

        // 搜尋資料
        [HttpPost()]
        public JsonResult SearchBook(Model.SearchArg arg)
        {
            Service.BookDataService bookDataService = new Service.BookDataService();
            return Json(bookDataService.GetBookDataWithConditions(arg));
        }


        // 顯示新增的頁面
        public ActionResult AddNewBook()
        {
            Service.BookClassService bookClassService = new Service.BookClassService();
            return View();
        }

        // 新增一筆新的BOOK_DATA進入資料庫
        [HttpPost()]
        public ActionResult CreateNewBook(Model.BookData bookData)
        {
            Service.BookDataService bookDataService = new Service.BookDataService();
            bookDataService.AddNewBook(bookData);
            return Redirect("/");
        }

        // 回傳編輯該BOOK_ID書籍的頁面
        public ActionResult EditBookData()
        {
            return View();
        }

        // 獲得該筆Book Data的資料
        [HttpPost()]
        public JsonResult EditBookData(int id)
        {
            Service.BookDataService bookDataService = new Service.BookDataService();
            Model.BookData bookData = bookDataService.GetBookData(id);
            return Json(bookData);
        }

        // 將編輯頁面做出的更動傳入資料庫
        [HttpPost()]
        public ActionResult UpdateBookData(Model.BookData bookData)
        {
            Service.BookDataService bookDataService = new Service.BookDataService();
            bookDataService.EditBook(bookData);
            return Redirect("/");
        }

        // 獲得該本書的目前借用狀況
        [HttpPost()]
        public JsonResult CurrentBookStatus(int id)
        {
            Service.BookDataService bookDataService = new Service.BookDataService();
            string status = bookDataService.GetCurrentBookStatus(id);
            return Json(status);
        }

        // 根據BOOK_ID刪除該筆資料
        [HttpPost()]
        public void DeleteBookData(int id)
        {
            Service.BookDataService bookDataService = new Service.BookDataService();
            bookDataService.DeleteBookData(id);
        }
    }
}