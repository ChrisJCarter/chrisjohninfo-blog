using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Entities = ChrisJohnInfo.Blog.Repositories.Dapper.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace ChrisJohnInfo.Blog.Repositories.Dapper
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDbConnection _connection;
        private readonly IMapper _mapper;

        public AdminRepository(IDbConnection connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        public async Task<Author> GetAuthorAsync(int authorId)
        {
            return _mapper.Map<Author>(await _connection.GetAsync<Entities.Author>(authorId));
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return (await _connection.GetListAsync<Entities.Author>()).Select(x => _mapper.Map<Author>(x));
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            var authorEntity = _mapper.Map<Entities.Author>(author);
            authorEntity.AuthorId = await _connection.InsertAsync(authorEntity) ?? -1;
            return _mapper.Map<Author>(authorEntity);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            var authorEntity = _mapper.Map<Entities.Author>(author);
            await _connection.UpdateAsync(authorEntity);
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            await _connection.DeleteAsync<Entities.Author>(authorId);
        }

        public async Task<Post> GetPostAsync(Guid postId)
        {

            return _mapper.Map<Post>(await _connection.GetAsync<Entities.Post>(postId));
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return (await _connection.GetListAsync<Entities.Post>()).Select(x => _mapper.Map<Post>(x));
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            var postEntity = _mapper.Map<Entities.Post>(post);
            postEntity.PostId = Guid.NewGuid();
            await _connection.InsertAsync<Guid, Entities.Post>(postEntity);
            return _mapper.Map<Post>(postEntity);
        }

        public async Task UpdatePostAsync(Post post)
        {
            var postEntity = _mapper.Map<Entities.Post>(post);
            await _connection.UpdateAsync(postEntity);
        }

        public async Task DeletePostAsync(Guid postId)
        {
            await _connection.DeleteAsync<Entities.Post>(postId);
        }
    }
}
