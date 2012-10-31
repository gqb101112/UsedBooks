using System;
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
        Entities Usedb = new Entities();
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
            return View();
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
                    Usedb.SaveChanges();
                    int a = 0;

                    return RedirectToAction("Index");
                }
                else
                    return View(data);
                
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /BooksManager/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Book book = Usedb.Book.Find(id);
            return View(book);
        }

        //
        // POST: /BooksManager/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                Usedb.Entry(book).State = EntityState.Modified;
                Usedb.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(book);
        }

        //
        // GET: /BooksManager/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Book book = Usedb.Book.Find(id);
            return View(book);
        }

        //
        // POST: /BooksManager/Delete/5

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Book book = Usedb.Book.Find(id);
                Usedb.Book.Remove(book);
                Usedb.SaveChanges();
                
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

      
    }
}
