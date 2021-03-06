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
                           select use;
                string name = "";
                
                foreach (User u in user)
                {
                    name = u.UserName;
                    order.UserID = u.UserID.ToString();

                }
                order.User = name;
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
                int Uid = Convert.ToInt32(orders.UserID);
                var users = from user in Usedb.User
                            where user.UserID == Uid
                            select user;
                foreach (var i in users)
                {
                    TempData["phone"] = i.Phone;
                }
                Order order = new Order();
                order.OrderDate = System.DateTime.Now;
                order.UserID = Convert.ToInt32(orders.UserID);
                order.BID = Convert.ToInt32(orders.BookID);
                order.Phone = data.phone;
                order.BName = orders.Name;
                Usedb.Order.Add(order);
                Usedb.SaveChanges();
                return View(orders);
            }
            return View();
        }
        
        public ActionResult BookPublic()
        {

            return View();
        }
        [HttpPost]
        
        public ActionResult BookPublic(BookN data)
        {
            BookNeed bookn = new BookNeed();
            bookn.BName = data.BookName;
            bookn.price = data.Price;
            bookn.Phone = data.Phone;
            bookn.DataTime = System.DateTime.Now;
            
            Usedb.BookNeed.Add(bookn);
            Usedb.SaveChanges();
            return View("BookPublic");
        }

        public ActionResult BookNeedShow(string sortOrder, int page = 1)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            const int maxRecords = 18;// 每页最大数目
            var booksN = from book in Usedb.BookNeed
                         orderby book.BookNID ascending
                         select book;
            var currentPage = page <= 0 ? 1 : page;
            return PartialView (booksN.ToPagedList(currentPage, maxRecords));
            
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
        public ActionResult ShopDetail(int id,string Uname,string date)
        {
            var books = from b in Usedb.Book
                        where b.UserID ==id 
                        select b;
            List<ShopDetaill> shops = new List<ShopDetaill>();
            foreach (Book b in books)
            {
                ShopDetaill shop = new ShopDetaill();
                shop.Autnor = b.Author;
                shop.BookID = b.BookID.ToString();
                shop.BookName = b.Name;
                shop.BookNum = books.Count().ToString();
                shop.OpenDate = date;
                shop.Price = b.Price;
                shop.Publish = b.Publish;
                shop.ShopName = Uname + "的书店";
                shop.UserName = Uname;
                shops.Add(shop);

            
            }
            return PartialView(shops);
        }

        
    }
}
