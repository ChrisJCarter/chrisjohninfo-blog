using System;

namespace AutoMapperProjectTo.Dto
{
    public class PostDto
    {
        public Guid PostId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}
