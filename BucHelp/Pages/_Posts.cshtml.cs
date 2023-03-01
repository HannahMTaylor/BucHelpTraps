using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BucHelp.Pages
{
    public class _PostsModel : PageModel
    {
        //testing
        public List<string> Animals = new List<string>();

        public void OnGet()
        {
            Animals.AddRange(new[] { "cow", "dog", "cat" });
        }

        //public IActionResult OnGetPartial() => new PartialViewResult
        //{
        //    ViewName = "_Posts.cshtml"
        //    //can add a ViewData to pass data along
        //};
    }
}
