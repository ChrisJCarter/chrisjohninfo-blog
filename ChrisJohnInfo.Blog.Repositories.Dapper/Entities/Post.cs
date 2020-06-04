using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisJohnInfo.Blog.Repositories.Dapper.Entities
{
    [Table(nameof(Post))]
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DatePublished { get; set; }
        public string RenderedHtml { get; set; }
    }
}
