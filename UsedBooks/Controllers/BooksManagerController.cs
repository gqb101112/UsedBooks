﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsedBook.Models;
using UsedBooks.Models;

namespace UsedBooks.Controllers
{   
    public class BooksManagerController : Controller
    {
        UsedBookEntities2 Usedb = new UsedBookEntities2();
        //
        // GET: /BooksManager/
        
        [Authorize]
        public ActionResult Index()
        {
            int Uid =Convert.ToInt32( Session["Uid"]);
            var books = from b in Usedb.Book
                        where b.UserID==Uid
                        select b;
            return View(books.ToList());
        }

        public ActionResult BookSold()
        {
            int Uid = Convert.ToInt32(Session["Uid"]);
            var books = from b in Usedb.Book
                        where b.UserID == Uid && b.BookState=="false"
                        select b;
            return View(books.ToList());
        }

        //
        // GET: /BooksManager/Details/5
       
        public ActionResult Details(int id)
        {
            Book book = Usedb.Book.Find(id);
            return View(book);
        }

        //
        // GET: /BooksManager/Create
        [Authorize]
        public ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /BooksManager/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(BookUp data)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Book book = new Book();
                    book.Author = data.Author;
                    book.Category = data.Category;
                    book.Publish = data.Publish;
                    book.Price = data.Price;
                    book.OldLevel = data.OldLevel;
                    book.Name = data.Name;
                    book.UserID = Convert.ToInt32(Session["Uid"]);
                    book.TotalNum = Convert.ToInt32(data.TotalNum);
                    book.Categories = data.Categories;
                    book.BookState="true";
                    Usedb.Book.Add(book);
                    User user = Usedb.User.Find(Session["Uid"]);
                    user.BookNum = (Convert.ToInt32(user.BookNum) + 1) + "";
                    Usedb.SaveChanges();
                    int a = 0;

                    return RedirectToAction("PersonalShop");
                }
                else
                    return View(data);
                
            }
            catch
            {
                return View();
            }
        }
        
        
        [Authorize]
        public ActionResult Edit(int id)
        {
            Book book = Usedb.Book.Find(id);
            return View(book);
        }

    

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Book book,int id)
        {
            
                //Usedb.Entry(book).State = EntityState.Modified;
            Book books = Usedb.Book.Find(id);
            books.Categories = book.Categories;
            books.Category = book.Category;
            books.Name = book.Name;
            books.OldLevel = book.OldLevel;
            books.Price = book.Price;
            books.Publish = book.Publish;
            books.TotalNum = book.TotalNum;
            books.Author = book.Author;
               
                Usedb.SaveChanges();
                return RedirectToAction("PersonalShop");
           
            
            //return View(book);
        }

        
        [Authorize]
        public ActionResult Delete(int id)
        {
            Book book = Usedb.Book.Find(id);
            Usedb.Book.Remove(book);
            Usedb.SaveChanges();
            User user = Usedb.User.Find(Session["Uid"]);
            user.BookNum = (Convert.ToInt32(user.BookNum) - 1) + "";
            TryUpdateModel(user);
            Usedb.SaveChanges();
            return RedirectToAction("PersonalShop");
        }

       

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Book book = Usedb.Book.Find(id);
                Usedb.Book.Remove(book);
                Usedb.SaveChanges();
                User user = Usedb.User.Find(Session["Uid"]);
                user.BookNum = (Convert.ToInt32(user.BookNum) - 1) + "";
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
      
        [Authorize]
        public ActionResult POrder()
        {

            return View();
        }
        [Authorize]
        public ActionResult PersonalShop()
        {
            int Uid = Convert.ToInt32(Session["Uid"]);
            var user = from u in Usedb.User
                       where u.UserID == Uid
                       select u;
            return View(user);
        }
        [Authorize]
        public ActionResult PersonalEdit()
        {
            User user = Usedb.User.Find(Convert.ToInt32(Session["Uid"]));
            return View(user);
        }
        [HttpPost]
        [Authorize]
        public ActionResult PersonalEdit(User user)
        {
            User users = Usedb.User.Find(Session["Uid"]);
            users.StoreName = user.StoreName;
            users.UserName = user.UserName;
            users.Phone = user.Phone;
            users.email = user.email;

            Usedb.SaveChanges();
            return RedirectToAction("PersonalShop");
        }

        [Authorize]
       
        public ActionResult BookStateChange(int id,int bid)//id=0表示 待售BookState=true   id=1 表示 已售  bid表示书本ID
        { 
            Book book = Usedb.Book.Find(bid);
            if (id == 0)
                book.BookState = "true";
            else
                book.BookState = "false";
            Usedb.SaveChanges();

            return RedirectToAction("PersonalShop");
        }

        [Authorize]
        public ActionResult MyBookOrder()
        {
            int id = Convert.ToInt32(Session["Uid"]);
            var orders = from order in Usedb.Order
                         where order.UserID == id
                         select order;

            return View(orders.ToList());
        }
    }
}
