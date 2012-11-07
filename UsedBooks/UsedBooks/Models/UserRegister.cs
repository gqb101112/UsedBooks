using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UsedBook.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage="用户名必须填")]
        public string UserName { get; set; }
        [Required(ErrorMessage="邮箱必须填")]
        
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4})$",
                ErrorMessage = "请输入正确的Email.")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsUserExists", "Admin",ErrorMessage = "用户名已经存在！", HttpMethod = "POST")]


        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "以字母开头，长度在6~18之间，只能包含字符、数字和下划线")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
        public string StoreName { get; set; }
        [Required]
        public string RealName { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$",ErrorMessage="请输入10位的学号")]
        public string StudentID { get; set; }

    }
}