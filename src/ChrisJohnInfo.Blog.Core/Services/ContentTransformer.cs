using ChrisJohnInfo.Blog.Contracts.Interfaces;
using Markdig;

namespace ChrisJohnInfo.Blog.Core.Services
{
    public class ContentTransformer : IContentTransformer
    {
        public string Transform(string content)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var result = Markdown.ToHtml(content, pipeline);
            return result;
        }
    }
}
