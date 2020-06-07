using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Repositories.EntityFramework;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories.EntityFramework
{
    [TestFixture]
    public class BlogRepositoryTests : BlogRepositoryBaseTests
    {
        protected override IBlogRepository CreateRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChrisJohnInfoBlogContext>()
                .UseSqlServer(ConnectionString);
            var context = new ChrisJohnInfoBlogContext(optionsBuilder.Options);
            return new BlogRepository(context);
        }
    }
}
