using System;

namespace ChrisJohnInfo.Blog.Core.Services
{
    // Contains methods and properties that can be used within a blog post.
    public class RazorTemplateHelper
    {
        public string Host { get; set; }
        public Guid PostId { get; set; }
        public string Static(string resource)
        {
            return @$"{Host}/posts/{PostId}/static/{resource}";
        }
    }
}