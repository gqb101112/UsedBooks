using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UsedBooks.Models
{
    public class Orders 
    {
        [Required(ErrorMessage="请填上你的联系方式")]
        [RegularExpression(@"^(130|131|132|133|134|135|136|137|138|187)\d{8}$", ErrorMessage="输入正确的手机号码")]
        public string phone { get; set; }

        public string Name { get; set; }//书本名
        
        public string Author { get; set; }
        public string Publish { get; set; }
        public string OldLevel { get; set; }
        public string date { get; set; }
        public string Price { get; set; }
        public string User { get; set; }//用户名
        public string UserID { get; set; }
        public string TotalNum { get; set; }
        public string BookID { get; set; }
        public string StoreName { get; set; }
    }
}
