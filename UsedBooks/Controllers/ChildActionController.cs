using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsedBooks.Models;

namespace UsedBooks.Controllers
{
    public class ChildActionController : Controller
    {
        //
        // GET: /ChildAction/

        UsedBookEntities2 Usedb = new UsedBookEntities2();
        [ChildActionOnly]
        public PartialViewResult AllBooksReturn()
        {
            
            var books = from b in Usedb.Book
                        select b;

            
            return PartialView(books);
        }

    }
}
