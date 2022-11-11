using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MushroomWebsite.Models;
using MushroomWebsite.Data;
using Serilog;
using System.Data;


namespace MushroomWebsite.Pages.Mushrooms
{
    public class CreateModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        readonly ILogger _log = Log.ForContext<CreateModel>();

        [BindProperty]
        public Mushroom Mushroom { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                await _db.Mushrooms.AddAsync(Mushroom);
                await _db.SaveChangesAsync();
                _log.Information("Mushroom added");
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            
            return RedirectToPage("Index");
        }
    }
}
