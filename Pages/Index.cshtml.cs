using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MushroomWebsite.Models;
using Serilog;

namespace MushroomWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private ILogger _log = Log.ForContext<RegisterModel>();

        [BindProperty]
        public User UserData { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            

            //redirect to account page
            return Page();
        }
    }
}
