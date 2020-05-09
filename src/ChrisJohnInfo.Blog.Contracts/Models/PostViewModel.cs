using System;
using System.Collections.Generic;
using System.Text;

namespace ChrisJohnInfo.Blog.Contracts.Models
{
    public class PostViewModel
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
