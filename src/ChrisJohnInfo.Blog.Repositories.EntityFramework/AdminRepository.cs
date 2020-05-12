using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities = ChrisJohnInfo.Blog.Repositories.EntityFramework.Entitites;

namespace ChrisJohnInfo.Blog.Repositories.EntityFramework
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ChrisJohnInfoBlogContext _context;
        private readonly IMapper _mapper;
        public AdminRepository(ChrisJohnInfoBlogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Author> GetAuthorAsync(int authorId)
        {
            return await _mapper.ProjectTo<Author>(_context.Authors.Where(a => a.AuthorId == authorId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _mapper.ProjectTo<Author>(_context.Authors).ToListAsync();
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            var entity = _mapper.Map<Entities.Author>(author);
            await _context.Authors.AddAsync(entity);
            await _context.SaveChangesAsync();
            author.AuthorId = entity.AuthorId;
            return author;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            var entity = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == author.AuthorId);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity with id {author.AuthorId} was not found!");
            }
            
            _mapper.Map(author, entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            var entity = await _context.Authors.FindAsync(authorId); 
            _context.Authors.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPostAsync(Guid postId)
        {
            return _mapper.Map<Post>(await _context.Posts.FindAsync(postId));
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _mapper.ProjectTo<Post>(_context.Posts).ToListAsync();
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            var entity = _mapper.Map<Entities.Post>(post);
            entity.PostId = Guid.NewGuid();
            await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();
            post.PostId = entity.PostId;
            return post;
        }

        public async Task UpdatePostAsync(Post post)
        {
            var entity = await _context.Posts.FindAsync(post.PostId);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity {post.PostId} not found!");
            }

            _mapper.Map(post, entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Guid postId)
        {
            _context.Posts.Remove(await _context.Posts.FindAsync(postId));
            await _context.SaveChangesAsync();
        }
    }
}
