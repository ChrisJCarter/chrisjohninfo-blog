using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace ChrisJohnInfo.Blog.Repositories.EntityFramework
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ChrisJohnInfoBlogContext _context;

        public AdminRepository(ChrisJohnInfoBlogContext context)
        {
            _context = context;
        }

        public async Task<Author> GetAuthorAsync(int authorId)
        {
            var entity = await _context.Authors.FirstOrDefaultAsync(x => x.AuthorId == authorId);
            if (entity == null)
            {
                return null;
            }
            var model = new Author();
            model.AuthorId = entity.AuthorId;
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.NickName = entity.NickName;
            return model;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            var entities = await _context.Authors.ToListAsync();
            var result = new List<Author>();
            if (entities == null || !entities.Any())
            {
                return result;
            }

            foreach (var entity in entities)
            {
                var model = new Author();
                model.AuthorId = entity.AuthorId;
                model.FirstName = entity.FirstName;
                model.LastName = entity.LastName;
                model.NickName = entity.NickName;
                result.Add(model);
            }

            return result;
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            var entity = new Entitites.Author();
            entity.FirstName = author.FirstName;
            entity.LastName = author.LastName;
            entity.NickName = author.NickName;
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
            entity.FirstName = author.FirstName;
            entity.LastName = author.LastName;
            entity.NickName = author.NickName;
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
            var entity = await _context.Posts.FindAsync(postId);
            if (entity == null)
            {
                return null;
            }

            var model = new Post
            {
                AuthorId = entity.AuthorId,
                Content = entity.Content,
                DatePublished = entity.DatePublished,
                Title = entity.Title,
                PostId = entity.PostId
            };
            return model;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            var entities = await _context.Posts.ToListAsync();
            
            if (entities == null)
            {
                return new List<Post>();
            }

            return entities.Select(entity => new Post
            {
                AuthorId = entity.AuthorId,
                Content = entity.Content,
                DatePublished = entity.DatePublished,
                Title = entity.Title,
                PostId = entity.PostId
            });
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            var entity = await _context.Posts.FindAsync(post.PostId);
            if (entity != null)
            {
                throw new InvalidOperationException($"Entity {post.PostId} already exists!");
            }

            entity = new Entitites.Post();
            entity.AuthorId = post.AuthorId;
            entity.Content = post.Content;
            entity.Title = post.Title;
            entity.DatePublished = post.DatePublished;
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

            entity.AuthorId = post.AuthorId;
            entity.Content = post.Content;
            entity.Title = post.Title;
            entity.DatePublished = post.DatePublished;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Guid postId)
        {
            _context.Posts.Remove(await _context.Posts.FindAsync(postId));
            await _context.SaveChangesAsync();
        }
    }
}
