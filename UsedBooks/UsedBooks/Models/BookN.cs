using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UsedBooks.Models
{
    public class BookN
    {
        [Required(ErrorMessage="请写上书名")]
        public string BookName { get; set; }
        [Required(ErrorMessage="请写上价格")]
        [RegularExpression(@"^[0-9]*[1-9][0-9]*$", ErrorMessage = "请填写正确的价格")]

        public string Price { get; set; }
        [Required(ErrorMessage="请填写联系方式")]
        [RegularExpression(@"^1[0-9]{10}$",ErrorMessage="请输入正确的手机号码")]
        public string Phone { get; set; }
    }
}