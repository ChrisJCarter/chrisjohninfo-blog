using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Repositories.Dapper;
using Microsoft.Data.SqlClient;
using NUnit.Framework;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories.Dapper
{
    [TestFixture]
    public class BlogRepositoryTests : BlogRepositoryBaseTests
    {
        protected override IBlogRepository CreateRepository()
        {
            return new BlogRepository(new SqlConnection(ConnectionString));
        }
    }
}
