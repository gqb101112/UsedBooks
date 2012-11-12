using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UsedBook.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage="登录名必须填写")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4})$",
               ErrorMessage = "请输入正确的Email")]
        public string Email { get; set; }
        [Required(ErrorMessage="密码必须填写")]
        public string Password { get; set; }

        

    }
}