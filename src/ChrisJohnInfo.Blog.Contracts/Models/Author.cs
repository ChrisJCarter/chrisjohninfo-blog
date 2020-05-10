using System;
using System.Collections.Generic;
using System.Text;

namespace ChrisJohnInfo.Blog.Contracts.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
    }
}
