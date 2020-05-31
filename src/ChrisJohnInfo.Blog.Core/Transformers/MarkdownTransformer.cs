using ChrisJohnInfo.Blog.Contracts.Interfaces;
using Markdig;
using Markdig.SyntaxHighlighting;
using System;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Core.Transformers
{
    public class MarkdownTransformer : IContentTransformer
    {
        private readonly MarkdownPipeline _markdown;

        public MarkdownTransformer()
        {
            _markdown = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseSyntaxHighlighting()
                .Build();
        }

        public Task<string> TransformAsync(Guid postId, string content) => Task.FromResult(Markdown.ToHtml(content, _markdown));
    }
}
