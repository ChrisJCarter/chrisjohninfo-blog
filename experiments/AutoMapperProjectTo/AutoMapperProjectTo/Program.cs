using System;
using AutoMapper;
using AutoMapperProjectTo.Dto;
using AutoMapperProjectTo.Entities;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace AutoMapperProjectTo
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>();
                cfg.CreateMap<Post, PostDto>();
            });
            
            var mapper = new Mapper(configuration);

            using (var context = new ChrisJohnInfoBlogContext("server=.;database=ChrisJohnInfoBlog;trusted_connection=true;"))
            {
                var authors = context.Author.ProjectTo<AuthorDto>().ToList();
                foreach (var author in authors)
                {
                    Console.WriteLine(author);
                }
            }

        }
    }
}
