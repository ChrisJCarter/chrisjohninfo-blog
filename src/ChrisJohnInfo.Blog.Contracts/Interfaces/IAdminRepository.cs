using ChrisJohnInfo.Blog.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Contracts.Interfaces
{
    public interface IAdminRepository
    {
        Task<Author> GetAuthorAsync(int authorId);
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> CreateAsync(Author author);
        Task UpdateAuthor(Author author);
        Task DeleteAuthor(int authorId);
    }
}
