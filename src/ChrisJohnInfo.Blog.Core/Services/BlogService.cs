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
            return (await _blogRepository.GetPosts(publishedOnly: true))
                .Select(x => new PostViewModel
                {
                    AuthorName = x.AuthorName,
                    Content = _contentTransformer.Transform(x.Content),
                    Title = x.Title,
                    DatePublished = x.DatePublished,
                    PostId = x.PostId
                });
        }

        public async Task<PostViewModel> GetPost(Guid postId)
        {
            var x = await _blogRepository.GetPost(postId);
            return new PostViewModel
            {
                AuthorName = x.AuthorName,
                Content = _contentTransformer.Transform(x.Content),
                Title = x.Title,
                DatePublished = x.DatePublished,
                PostId = x.PostId
            };
        }
    }
}
