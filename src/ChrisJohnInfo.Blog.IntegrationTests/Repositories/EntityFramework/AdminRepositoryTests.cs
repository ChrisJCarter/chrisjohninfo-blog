using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Repositories.EntityFramework;
using ChrisJohnInfo.Blog.Repositories.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories.EntityFramework
{
    [TestFixture]
    public class AdminRepositoryTests
    {
        private readonly IConfiguration _configuration;

        private ChrisJohnInfoBlogContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChrisJohnInfoBlogContext>()
                .UseSqlServer(_configuration.GetConnectionString("ChrisJohnInfoBlog"));
            return new ChrisJohnInfoBlogContext(optionsBuilder.Options);
        }
        private IAdminRepository CreateRepository()
        {
            return new AdminRepository(CreateContext());
        }

        public AdminRepositoryTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets("a2bf567b-768c-4818-b9e4-9ad84fd44eb1")
                .Build();
        }

        [Test]
        public async Task PostCrud()
        {
            var context = CreateContext();
            var repo = CreateRepository();

            // create the author
            var author = await repo.CreateAuthorAsync(new Author {FirstName = "Jack", LastName = "Wagon", NickName = "JagOff"});

            // create a post
            var post = await repo.CreatePostAsync(new Post
            {
                AuthorId = author.AuthorId,
                Title = "Test post",
                Content = "Jack Wagon's are beautiful ",
                DatePublished = new DateTime(1970, 6, 18)
            });
            Assert.That(post.PostId, Is.Not.EqualTo(Guid.Empty));

            // update a post
            post.Content = "You are blah";
            await repo.UpdatePostAsync(post);
            var updated = await repo.GetPostAsync(post.PostId);
            Assert.That(updated.Content, Is.EqualTo("You are blah"));

            // delete it!
            await repo.DeletePostAsync(updated.PostId);

            // make sure it's gone
            var deletedPost = await repo.GetPostAsync(updated.PostId);
            Assert.That(deletedPost, Is.Null);

            // remove the author
            await repo.DeleteAuthorAsync(author.AuthorId);
        }

        [Test]
        public async Task AuthorCrud()
        {
            var context = CreateContext();
            var repo = CreateRepository();

            // Penelope should not exist yet!
            var existing = await (from a in context.Authors
                                  where a.FirstName == "Penelope" && a.LastName == "Thyne"
                                  select a).SingleOrDefaultAsync();

            Assert.That(existing, Is.Null, "Penelope does not exist");

            // So let's create her!
            var newAuthor = new Author();
            newAuthor.FirstName = "Penelope";
            newAuthor.LastName = "Carter";
            newAuthor.NickName = "Penelopenis";
            var created = await repo.CreateAuthorAsync(newAuthor);
            Assert.That(created.AuthorId, Is.GreaterThan(0));

            // Fix her last name!
            var goingToUpdate = await repo.GetAuthorAsync(created.AuthorId);
            goingToUpdate.LastName = "Thyne";
            await repo.UpdateAuthorAsync(goingToUpdate);

            // Did she update?
            var updated = await repo.GetAuthorAsync(goingToUpdate.AuthorId);
            Assert.That(updated.LastName, Is.EqualTo("Thyne"));

            // Let's get rid of her
            await repo.DeleteAuthorAsync(updated.AuthorId);

            // Is she gone?
            existing = await (from a in context.Authors
                              where a.FirstName == "Penelope" && a.LastName == "Thyne"
                              select a).SingleOrDefaultAsync();
            Assert.That(existing, Is.Null);


        }
    }
}
