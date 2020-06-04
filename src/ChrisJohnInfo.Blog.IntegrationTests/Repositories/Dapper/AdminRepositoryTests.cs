using System;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.Models;
using ChrisJohnInfo.Blog.Repositories.Dapper;
using Microsoft.Data.SqlClient;
using NUnit.Framework;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories.Dapper
{
    [TestFixture]
    public class AdminRepositoryTests
    {
        public IAdminRepository CreateRepository()
        {
            var configurationProvider = new MapperConfiguration(c => c.AddProfile<AdminProfile>());
            var mapper = new Mapper(configurationProvider);
            return new AdminRepository(CreateConnection(), mapper);
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection("server=.;database=ChrisJohnInfoBlog;trusted_connection=true;");
        }

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
