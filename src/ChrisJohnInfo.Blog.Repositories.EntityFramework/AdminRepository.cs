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

        public async Task<Author> CreateAsync(Author author)
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

        public async Task UpdateAuthor(Author author)
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

        public async Task DeleteAuthor(int authorId)
        {
            var entity = await _context.Authors.FindAsync(authorId); 
            _context.Authors.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
