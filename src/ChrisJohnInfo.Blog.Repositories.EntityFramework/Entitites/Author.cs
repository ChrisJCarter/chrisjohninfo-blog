using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChrisJohnInfo.Blog.Repositories.EntityFramework.Entitites
{
    [Table(("Author"))]
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
