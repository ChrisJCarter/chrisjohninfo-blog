using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using ChrisJohnInfo.Blog.Core.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Contracts.ViewModels;
using ChrisJohnInfo.Blog.Core.Transformers;

namespace ChrisJohnInfo.Blog.Tests
{
    [TestFixture]
    public class BlogServiceTests
    {
        [Test]
        public async Task CanGetPosts()
        {
            var blogRepository = new Mock<IBlogRepository>(MockBehavior.Strict);
            blogRepository
                .Setup(s => s.GetPosts(false))
                .ReturnsAsync(new List<PostViewModel>{new PostViewModel{Title = "Hello World"}});
            var service = new BlogService(blogRepository.Object, new MarkdownTransformer(), new RazorTransformer());

            var posts = await service.GetPosts();

            Assert.That(posts.Count(), Is.EqualTo(1));
            Assert.That(posts.ElementAt(0).Title, Is.EqualTo("Hello World"));
            blogRepository
                .Verify(s => s.GetPosts(false), Times.Once);
        }
    }
}
