//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UsedBooks.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Book = new HashSet<Book>();
            this.Order = new HashSet<Order>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
        public int StudentID { get; set; }
        public string StoreName { get; set; }
        public System.DateTime DataTime { get; set; }
        public string BookNum { get; set; }
        public string College { get; set; }
    
        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
