using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MushroomWebsite.Data;
using MushroomWebsite.Models;

namespace MushroomWebsite.Areas.User.Pages.Mushrooms
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        public IEnumerable<Mushroom> Mushrooms { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Mushrooms = _db.Mushrooms;
        }
    }
}
