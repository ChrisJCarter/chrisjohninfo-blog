using AutoMapper;
using Entities = ChrisJohnInfo.Blog.Repositories.EntityFramework.Entitites;
using Models = ChrisJohnInfo.Blog.Contracts.Models;

namespace ChrisJohnInfo.Blog.Repositories.EntityFramework
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Entities.Author, Models.Author>().ReverseMap();
            CreateMap<Entities.Post, Models.Post>().ReverseMap();
        }
    }
}
