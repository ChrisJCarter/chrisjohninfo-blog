using ChrisJohnInfo.Blog.Contracts.Interfaces;
using System;
using System.Threading.Tasks;
using RazorLight;

namespace ChrisJohnInfo.Blog.Core.Transformers
{
    public class RazorTransformer : IContentTransformer
    {
        public async Task<string> TransformAsync(Guid postId, string content)
        {
            var razor = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(RazorTransformer))
                .Build();

            var razorTemplateHelper = new RazorTemplateHelper {Host = "", PostId = postId};
            return await razor.CompileRenderStringAsync(postId.ToString(), content, razorTemplateHelper);
        }
    }
}
