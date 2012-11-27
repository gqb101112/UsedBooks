using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using UsedBook.Models;
using UsedBooks.Controllers;
using UsedBooks.Models;

namespace UsedBook.Controllers
{
    public class AdminController :Controller
    {
        UsedBookEntities1 Usedb = new UsedBookEntities1();
        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult IsUserExists(string Email)
        {
            int c = Usedb.User.Where(p => p.email == Email).Count();
            bool exists = c > 0;

            return Json(!exists, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Register(UserRegister data)
        {

            if (ModelState.IsValid)
            {
                var AccountUser = from u in Usedb.User
                                  where u.email == data.Email
                                  select u;
                if (AccountUser.Count() > 0)
                {
                    TempData["message"] = "该email已经被注册";
                    RedirectToAction("Register");
                }
                else
                {
                    User user = new User();
                    user.UserName = data.UserName;
                    if (data.StoreName.Equals(""))
                    {
                        user.StoreName = data.UserName;
                    }
                    else
                    {
                        user.StoreName = data.StoreName;
                    }

                    user.Phone = "12345678901";
                    user.Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(data.Password, "MD5");
                    user.RealName = data.RealName;
                    user.StudentID = Convert.ToInt32(data.StudentID);
                    user.email = data.Email;
                    user.DataTime = DateTime.Now;
                    user.College = data.College;
                    Usedb.User.Add(user);

                    Usedb.SaveChanges();
                    var User = from u in Usedb.User
                               where u.email == user.email
                               select u;
                    int Uid = 0;
                    foreach (var UName in AccountUser)
                    {
                       
                        Uid = UName.UserID;
                        
                    }
                    Session["Uid"] = Uid;
                }

            }
            else
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
        #region 登入  登出
        public ActionResult Login()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin data,string ReturnUrl)
        {
            string name = null;
            if (ModelState.IsValid)
            {
                string psd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(data.Password, "MD5");

                var AccountUser = from u in Usedb.User
                                  where u.email == data.Email && u.Password == psd
                                  select u;

                int Uid=0;
                string email=null;
                foreach (var UName in AccountUser)
                {
                    name = UName.UserName;
                    Uid = UName.UserID;
                    email = UName.email;
                }
                if (AccountUser.Count() > 0)
                {
                    FormsAuthentication.SetAuthCookie(name, true);
                    if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                        && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                    
                    {
                        Session["Uid"] = Uid;
                       // return Redirect(ReturnUrl);
                        return RedirectToAction("PersonalShop", "BooksManager");
                    }
                    else
                    {
                        Session["Uid"] = Uid;
                        return RedirectToAction("Index", "Home");
                    }
                    
                   
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
            //return Content("<script >alert('提交留言成功，谢谢对我们支持，我们会根据您提供联系方式尽快与您取的联系！');</script >", "text/html");


        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            FormsAuthentication.SignOut();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion


        #region 书本上传
        [Authorize]
        public ActionResult BookUpload()
        {
            return View();
        }
        /// <summary>
        /// 书籍上传
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BookUpload(BookUp data)
        {
            
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
                    Usedb.Book.Add(book);
                    Usedb.SaveChanges();
                    TempData["bookupload"] = "上传成功！";
                }
           
            return View("BookUpload");
        }
        #endregion
    }
}
