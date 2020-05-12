using System;
using System.Collections.Generic;

namespace AutoMapperProjectTo.Entities
{
    public partial class Post
    {
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DatePublished { get; set; }

        public virtual Author Author { get; set; }
    }
}
