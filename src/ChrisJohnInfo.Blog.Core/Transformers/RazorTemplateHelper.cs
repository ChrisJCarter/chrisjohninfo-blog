using System;

namespace ChrisJohnInfo.Blog.Core.Transformers
{
    public class RazorTemplateHelper
    {
        public string Host { get; set; }
        public Guid PostId { get; set; }

        public string Static(string resourceName) => $"{Host}/posts/{PostId}/static/{resourceName}";
    }
}