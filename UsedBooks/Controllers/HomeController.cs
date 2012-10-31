﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using UsedBooks.Models;
using PagedList;


namespace UsedBooks.Controllers
{
    public class HomeController : Controller
    {
        UsedBookEntities1 Usedb = new UsedBookEntities1();
        int bookId = 0;
        public ActionResult Index(string sortOrder,int page=1)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            sortOrder="BookID desc";
            const int maxRecords = 18;// 每页最大数目
            ViewBag.Message = "欢迎!";
            var books = from book in Usedb.Book
                        orderby book.BookID ascending 
                        select book;
            var currentPage = page <= 0 ? 1 : page;

            
            return View(books.ToPagedList(currentPage, maxRecords));
           
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
                var user = from use in Usedb.User
                           where use.UserID == b.UserID
                           select use.UserName;
                order.User = user.ToString();
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

        public ActionResult BookShop()
        {
            var users = from use in Usedb.User
                        select use;
            List<BookShop> shops=new List<BookShop>();
            foreach (User u in users)
            {
                BookShop shop=new BookShop();
                shop.User = u.UserName;
                shop.UserID = u.UserID.ToString();
                shop.OpenTime = u.DataTime.ToShortDateString();
                var books = from book in Usedb.Book
                            where book.UserID == u.UserID
                            select book;
                shop.BookNum = books.Count().ToString();
                shops.Add(shop);

            }
            
            return PartialView(shops);
        }
       
        
    }
}
