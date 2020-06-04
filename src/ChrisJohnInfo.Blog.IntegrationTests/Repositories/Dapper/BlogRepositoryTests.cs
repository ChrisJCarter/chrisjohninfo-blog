using System;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Repositories.Dapper;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories.Dapper
{
    [TestFixture]
    public class BlogRepositoryTests
    {
        private IBlogRepository CreateRepository()
        {
            return new BlogRepository(CreateConnection());
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection("server=.;database=ChrisJohnInfoBlog;trusted_connection=true");
        }

        [Test]
        public async Task CanGetPublishedPosts()
        {
            var repo = CreateRepository();
            var posts = await repo.GetPosts(publishedOnly: true);
            Assert.That(posts.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task CanGetSinglePost()
        {
            var repo = CreateRepository();
            var post = await repo.GetPost(Guid.Parse("29D77453-5A64-4D08-AF7B-002104063C6D"));
            Assert.That(post, Is.Not.Null);
            Assert.That(post.Title, Is.EqualTo("The Blog Project"));
        }
    }
}
