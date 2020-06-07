using AutoMapper;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Repositories.EntityFramework;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories.EntityFramework
{
    public class AdminRepositoryTests : AdminRepositoryBaseTests
    {
        protected override IAdminRepository CreateRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChrisJohnInfoBlogContext>()
                .UseSqlServer(ConnectionString);
            var context = new ChrisJohnInfoBlogContext(optionsBuilder.Options);
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BlogProfile>();
            }).CreateMapper();

            return new AdminRepository(context, mapper);
        }
    }
}
