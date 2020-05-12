using System;
using ChrisJohnInfo.Blog.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Contracts.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task CreateAuthorAsync(Author author);
        Task<Author> GetAuthorAsync(int authorId);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int authorId);
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> GetPostAsync(Guid postId);
        Task UpdatePostAsync(Post post);
        Task CreatePostAsync(Post post);
        Task DeletePostAsync(Guid postId);
    }
}
