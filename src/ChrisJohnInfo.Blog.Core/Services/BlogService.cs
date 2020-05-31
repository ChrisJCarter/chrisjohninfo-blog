using System;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IContentTransformer _contentTransformer;

        public BlogService(IBlogRepository blogRepository, IContentTransformer contentTransformer)
        {
            _blogRepository = blogRepository;
            _contentTransformer = contentTransformer;
        }

        public async Task<IEnumerable<PostViewModel>> GetPosts()
        {
            var publishedPosts = await _blogRepository.GetPosts(publishedOnly: true);
            var models = publishedPosts.Select(x => new PostViewModel
            {
                PostId = x.PostId,
                Content = x.Content,
                Title = x.Title,
                AuthorName = x.AuthorName,
                DatePublished = x.DatePublished
            }).ToList();

            foreach (var model in models)
            {
                model.Content = await _contentTransformer.Transform(model.PostId, model.Content);
            }

            return models;
        }

        public async Task<PostViewModel> GetPost(Guid postId)
        {
            var x = await _blogRepository.GetPost(postId);
            return new PostViewModel
            {
                AuthorName = x.AuthorName,
                Content = await _contentTransformer.Transform(x.PostId, x.Content),
                Title = x.Title,
                DatePublished = x.DatePublished,
                PostId = x.PostId
            };
        }
    }
}
