using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories
{
    [TestFixture]
    public abstract class AdminRepositoryBaseTests : RepositoryTest
    {
        protected abstract IAdminRepository CreateRepository();

        [Test]
        public async Task PostCrud()
        {
            var repo = CreateRepository();

            // create the author
            var author = await repo.CreateAuthorAsync(new Author { FirstName = "Jack", LastName = "Wagon", NickName = "JagOff" });

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
            var repo = CreateRepository();

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

        }
    }
}