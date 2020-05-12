using System;
using AutoMapper;
using AutoMapperProjectTo.Dto;
using AutoMapperProjectTo.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperProjectTo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>();
                cfg.CreateMap<Post, PostDto>();
            });
            
            // ProjectTo<T>() are extensions off of IMapper, using var on 
            // following line excludes the extension methods.
            IMapper mapper = new Mapper(configuration);

            await using var context = new ChrisJohnInfoBlogContext("server=.;database=ChrisJohnInfoBlog;trusted_connection=true;");
            var authors = await mapper.ProjectTo<AuthorDto>(context.Author).ToListAsync();
            Console.WriteLine($"Authors using mapper.ProjectTo<>(): {authors.Count}");
            var authors2 = await context.Author.ProjectTo<AuthorDto>(configuration).ToListAsync();
            Console.WriteLine($"Authors using IQueryable.ProjectTo<>(config): {authors2.Count}");
        }
    }
}
