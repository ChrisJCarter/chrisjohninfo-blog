using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories.EntityFramework
{
    [TestFixture]
    public class BlogRepositoryTests
    {
        private readonly IConfiguration _configuration;

        public BlogRepositoryTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets("a2bf567b-768c-4818-b9e4-9ad84fd44eb1", true)
                .Build();
        }
        [Test]
        public async Task GetPosts()
        {
            var connectionString = _configuration.GetConnectionString("ChrisJohnInfoBlog");
            Assert.That(connectionString, Is.EqualTo("server=.;database=ChrisJohnInfoBlog;trusted_connection=true;"));
            var context = new ChrisJohnInfoBlogContext(connectionString);
            var posts = await context.Posts.ToListAsync();
            Assert.That(posts.Count, Is.EqualTo(1));
            var post = posts.First();
            Assert.That(post.Title, Is.EqualTo("This is my post"));
        }
    }
}
