using Markdig;
using NUnit.Framework;
using System;

namespace ChrisJohnInfo.Blog.Tests
{
    [TestFixture]
    public class MarkdigTests
    {
        [Test]
        public void Blah()
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var result = Markdown.ToHtml("This is a text with some *emphasis*", pipeline);
            Console.WriteLine(result);
        }
    }
}
