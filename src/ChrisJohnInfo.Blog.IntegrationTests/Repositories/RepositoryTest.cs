using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories
{
    [TestFixture]
    public abstract class RepositoryTest
    {
        protected IConfiguration Configuration { get; }

        protected string ConnectionString { get; }

        protected RepositoryTest()
        {
            Configuration = new ConfigurationBuilder()
                .AddUserSecrets("a2bf567b-768c-4818-b9e4-9ad84fd44eb1")
                .Build();

            ConnectionString = Configuration["sql-ChrisJohnInfoBlog-001"];
        }
    }
}
