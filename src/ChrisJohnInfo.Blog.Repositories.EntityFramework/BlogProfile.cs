using AutoMapper;
using ChrisJohnInfo.Blog.Contracts.ViewModels;
using Models = ChrisJohnInfo.Blog.Contracts.Models;
using Entities = ChrisJohnInfo.Blog.Repositories.EntityFramework.Entitites;

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
