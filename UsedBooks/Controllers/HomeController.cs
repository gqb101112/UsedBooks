using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsedBooks.Models;


namespace UsedBooks.Controllers
{
    public class HomeController : Controller
    {
        Entities Usedb = new Entities();
        int bookId = 0;
        public ActionResult Index(int id)
        {
            

            ViewBag.Message = "欢迎!";
            var books = from book in Usedb.Book
                        
                        select book;
            PagedList<Book> bookes = Usedb.Book.ToPagedList(id ?? 1,18);
             

            return View(books);
           
        }
        public ActionResult Details(int id)
        {
            Book book = Usedb.Book.Find(id);
            return View(book);
        }
        public ActionResult Order(int id)
        {
            var books = from book in Usedb.Book
                        where book.BookID == id
                        select book;
            Orders order = new Orders();
            foreach(Book b in books)
            {
                order.Name=b.Name;
                order.BookID=id.ToString();
                order.OldLevel=b.OldLevel;
                order.Price=b.Price;
                order.Publish=b.Publish;
                order.User=b.UserID.ToString();
                order.Author=b.Author;
                order.TotalNum=b.TotalNum.ToString();


            }
            Session["order"] = order;
            
            return View(order);
        }
        [HttpPost]
        public ActionResult Order(Orders data)
        {
            Orders orders = (Orders)Session["order"];
            if (ModelState.IsValid)
            {
                int Uid = Convert.ToInt32(orders.User);
                var users = from user in Usedb.User
                            where user.UserID == Uid
                            select user;
                foreach (var i in users)
                {
                    TempData["phone"] = i.Phone;
                }
                Order order = new Order();
                order.OrderDate = System.DateTime.Now;
                order.UserID = Convert.ToInt32(orders.User);
                order.BID = Convert.ToInt32(orders.BookID);
                order.Phone = data.phone;
                order.BName = orders.Name;
                Usedb.Order.Add(order);
                Usedb.SaveChanges();
                return View(orders);
            }
            return View();
        }
        [Authorize]
        public ActionResult BookPublic()
        {

            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult BookPublic(BookN data)
        {
            BookNeed bookn = new BookNeed();
            bookn.BName = data.BookName;
            bookn.price = data.Price;
            bookn.Phone = data.Phone;
            bookn.DataTime = System.DateTime.Now;
            bookn.UserID =Convert.ToInt32(Session["Uid"]);
            Usedb.BookNeed.Add(bookn);
            Usedb.SaveChanges();
            return View("BookPublic");
        }

       
        public ActionResult About()
        {
            return View();
        }
    }
}
