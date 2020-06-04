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
        private readonly IAdminRepository _adminRepository;

        public BlogService(IBlogRepository blogRepository, MarkdownTransformer markdownTransformer, RazorTransformer razorTransformer, IAdminRepository adminRepository)
        {
            _blogRepository = blogRepository;
            _markdownTransformer = markdownTransformer;
            _razorTransformer = razorTransformer;
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<PostViewModel>> GetPosts()
        {
            var posts = (await _blogRepository.GetPosts(publishedOnly: true)).ToList();
            foreach (var post in posts)
            {
                post.Content = await GetContent(post);
            }
            return posts;
        }

        private async Task<string> GetContent(PostViewModel postView)
        {
            if (!String.IsNullOrEmpty(postView.RenderedHtml))
            {
                return postView.RenderedHtml;
            }

            var post = await _adminRepository.GetPostAsync(postView.PostId);
            var content = await _razorTransformer.TransformAsync(post.PostId, post.Content);
            content = await _markdownTransformer.TransformAsync(post.PostId, content);
            post.RenderedHtml = content;
            await _adminRepository.UpdatePostAsync(post);
            return content;

        }

        public async Task<PostViewModel> GetPost(Guid postId)
        {
            var post = await _blogRepository.GetPost(postId);
            post.Content = await GetContent(post);
            return post;
        }
    }
}
