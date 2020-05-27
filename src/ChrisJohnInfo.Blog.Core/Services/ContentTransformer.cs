using ChrisJohnInfo.Blog.Contracts.Interfaces;
using Markdig;
using Markdig.SyntaxHighlighting;

namespace ChrisJohnInfo.Blog.Core.Services
{
    public class ContentTransformer : IContentTransformer
    {
        public string Transform(string content)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseSyntaxHighlighting()
                .Build();
            var result = Markdown.ToHtml(content, pipeline);
            return result;
        }
    }
}
