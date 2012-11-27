using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UsedBook.Models
{
    public class BookUp
    {
        [Required(ErrorMessage = "书名必须填写")]
        public string Name { get; set; }
        [Required(ErrorMessage = "作者必须填写")]
        public string Author { get; set; }
        public string Publish { get; set; }
        public string OldLevel { get; set; }
        [Required(ErrorMessage="出售价格必填")]
        public string Price { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage="请填写书籍数量")]
        public string TotalNum { get; set; }
        public string Categories { get; set; }

        public string OldPrice { get; set; }
        public string Professional { get; set; }
        public string Course { get; set; }


      
    }   
}