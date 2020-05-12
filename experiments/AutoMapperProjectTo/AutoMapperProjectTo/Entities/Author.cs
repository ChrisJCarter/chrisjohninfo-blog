using System;
using System.Collections.Generic;

namespace AutoMapperProjectTo.Entities
{
    public partial class Author
    {
        public Author()
        {
            Post = new HashSet<Post>();
        }

        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
