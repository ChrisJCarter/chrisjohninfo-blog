using ChrisJohnInfo.Blog.Contracts.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ChrisJohnInfo.Blog.MvcUI.Areas.Admin.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
    }
}
