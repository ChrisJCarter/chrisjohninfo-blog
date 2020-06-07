using AutoMapper;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Repositories.Dapper;
using Microsoft.Data.SqlClient;

namespace ChrisJohnInfo.Blog.IntegrationTests.Repositories.Dapper
{
    public class AdminRepositoryTests : AdminRepositoryBaseTests
    {
        protected override IAdminRepository CreateRepository()
        {
            var configurationProvider = new MapperConfiguration(c => c.AddProfile<AdminProfile>());
            var mapper = new Mapper(configurationProvider);
            return new AdminRepository(new SqlConnection(ConnectionString), mapper);
        }
    }
}
