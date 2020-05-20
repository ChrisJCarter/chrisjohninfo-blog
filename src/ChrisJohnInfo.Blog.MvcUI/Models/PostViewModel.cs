using System;

namespace ChrisJohnInfo.Blog.MvcUI.Models
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DatePublished { get; set; }
        public string AuthorName { get; set; }
    }
}
