using System;
using ChrisJohnInfo.Blog.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Contracts.Interfaces
{
    public interface IAdminRepository
    {
        Task<Author> GetAuthorAsync(int authorId);
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> CreateAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int authorId);

        Task<Post> GetPostAsync(Guid postId);
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(Guid postId);
    }
}
