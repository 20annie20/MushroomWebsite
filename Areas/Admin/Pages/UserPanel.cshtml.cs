using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace MushroomWebsite.Areas.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class UserPanelModel : PageModel
    {
        public void OnGet()
        {
        } 
    }
}
