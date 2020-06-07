using ChrisJohnInfo.Blog.Contracts.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories
{
    public abstract class BlogRepositoryBaseTests : RepositoryTest
    {
        protected abstract IBlogRepository CreateRepository();

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