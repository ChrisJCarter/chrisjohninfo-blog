using System;

namespace ChrisJohnInfo.Blog.Contracts.ViewModels
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DatePublished { get; set; }
        public string AuthorName { get; set; }
        public string RenderedHtml { get; set; }
    }
}
