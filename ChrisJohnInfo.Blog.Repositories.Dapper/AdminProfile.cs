using Models = ChrisJohnInfo.Blog.Contracts.Models;

namespace ChrisJohnInfo.Blog.Repositories.Dapper
{
    public class AdminProfile : AutoMapper.Profile
    {
        public AdminProfile()
        {
            CreateMap<Entities.Author, Models.Author>().ReverseMap();
            CreateMap<Entities.Post, Models.Post>().ReverseMap();
        }
    }
}
