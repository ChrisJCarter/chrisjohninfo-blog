using ChrisJohnInfo.Blog.Contracts.Interfaces;
using System;
using System.Threading.Tasks;
using RazorLight;

namespace ChrisJohnInfo.Blog.Core.Transformers
{
    public class RazorTransformer : IContentTransformer
    {
        private readonly RazorLightEngine _razor;

        public RazorTransformer()
        {
            _razor = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(RazorTransformer))
                .Build();
        }

        public async Task<string> TransformAsync(Guid postId, string content)
        {
            var razorTemplateHelper = new RazorTemplateHelper {Host = "", PostId = postId};
            return await _razor.CompileRenderStringAsync(postId.ToString(), content, razorTemplateHelper);
        }
    }
}
