using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;

        public AdminService(IAdminRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _repo.GetAuthorsAsync();
        }

        public async Task CreateAuthorAsync(Author author)
        {
            await _repo.CreateAuthorAsync(author);
        }

        public async Task<Author> GetAuthorAsync(int authorId)
        {
            return await _repo.GetAuthorAsync(authorId);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            await _repo.UpdateAuthorAsync(author);
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            await _repo.DeleteAuthorAsync(authorId);
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _repo.GetPostsAsync();
        }
    }
}
