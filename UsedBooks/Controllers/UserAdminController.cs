using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsedBooks.Models;

namespace UsedBooks.Controllers
{
    public class UserAdminController : Controller
    {
        UsedBookEntities1 Usedb = new UsedBookEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ALogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin) {

            return View();
        }
    }
}
