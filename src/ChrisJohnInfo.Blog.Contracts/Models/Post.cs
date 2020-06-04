using System;

namespace ChrisJohnInfo.Blog.Contracts.Models
{
    public class Post
    {
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DatePublished { get; set; }
        public string RenderedHtml { get; set; }
    }
}
