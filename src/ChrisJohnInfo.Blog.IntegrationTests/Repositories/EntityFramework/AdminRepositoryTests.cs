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
        public async Task CanCreate()
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
            var created = await repo.CreateAsync(newAuthor);
            Assert.That(created.AuthorId, Is.GreaterThan(1));

            // Fix her last name!
            var goingToUpdate = await repo.GetAuthorAsync(created.AuthorId);
            goingToUpdate.LastName = "Thyne";
            await repo.UpdateAuthor(goingToUpdate);

            // Did she update?
            var updated = await repo.GetAuthorAsync(goingToUpdate.AuthorId);
            Assert.That(updated.LastName, Is.EqualTo("Thyne"));

            // Let's get rid of her
            await repo.DeleteAuthor(updated.AuthorId);

            // Is she gone?
            existing = await (from a in context.Authors
                              where a.FirstName == "Penelope" && a.LastName == "Thyne"
                              select a).SingleOrDefaultAsync();
            Assert.That(existing, Is.Null);


        }
    }
}
