using ChrisJohnInfo.Blog.Contracts.Interfaces;
using Markdig;
using Markdig.SyntaxHighlighting;
using RazorLight;
using System;
using System.Threading.Tasks;

namespace ChrisJohnInfo.Blog.Core.Services
{
    public class ContentTransformer : IContentTransformer
    {
        private readonly RazorLightEngine _razor;
        private readonly MarkdownPipeline _markdown;
        public ContentTransformer()
        {
            _razor = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(RazorTemplateHelper))
                .UseMemoryCachingProvider()
                .Build();

            _markdown = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseSyntaxHighlighting()
                .Build();
        }

        public async Task<string> Transform(Guid postId, string content)
        {
            var blogHelper = new RazorTemplateHelper
            {
                Host = "", // not sure i need this
                PostId = postId
            };
            
            // First pass, process post as a Razor template
            var result = await _razor.CompileRenderStringAsync("templateKey", content, blogHelper);
            
            // Second pass, process the results of the first pass via Markdig
            result = Markdown.ToHtml(result, _markdown);

            return result;
        }

        
    }
}
