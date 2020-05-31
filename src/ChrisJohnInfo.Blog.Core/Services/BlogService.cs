using System;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Contracts.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisJohnInfo.Blog.Core.Transformers;

namespace ChrisJohnInfo.Blog.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IContentTransformer _markdownTransformer;
        private readonly IContentTransformer _razorTransformer;
        public BlogService(IBlogRepository blogRepository, MarkdownTransformer markdownTransformer, RazorTransformer razorTransformer)
        {
            _blogRepository = blogRepository;
            _markdownTransformer = markdownTransformer;
            _razorTransformer = razorTransformer;
        }

        public async Task<IEnumerable<PostViewModel>> GetPosts()
        {
            var posts = (await _blogRepository.GetPosts(publishedOnly: true)).ToList();
            foreach (var post in posts)
            {
                post.Content = await _razorTransformer.TransformAsync(post.PostId, post.Content);
                post.Content = await _markdownTransformer.TransformAsync(post.PostId, post.Content);
            }
            return posts;
        }

        public async Task<PostViewModel> GetPost(Guid postId)
        {
            var x = await _blogRepository.GetPost(postId);
            return new PostViewModel
            {
                AuthorName = x.AuthorName,
                Content = await _markdownTransformer.TransformAsync(x.PostId, x.Content),
                Title = x.Title,
                DatePublished = x.DatePublished,
                PostId = x.PostId
            };
        }
    }
}
