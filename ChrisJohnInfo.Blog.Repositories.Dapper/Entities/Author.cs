using Dapper;

namespace ChrisJohnInfo.Blog.Repositories.Dapper.Entities
{
    [Table(nameof(Author))]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
    }
}
