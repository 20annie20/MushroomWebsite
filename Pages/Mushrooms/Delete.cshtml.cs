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
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        readonly ILogger _log = Log.ForContext<EditModel>();

        [BindProperty]
        public Mushroom Mushroom { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            try
            {
                Mushroom = _db.Mushrooms.Find(id);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }

        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var mushroomFromDb = _db.Mushrooms.Find(Mushroom.Id);
                if (mushroomFromDb != null)
                {
                    _db.Mushrooms.Remove(mushroomFromDb);
                    await _db.SaveChangesAsync();
                    _log.Information("Mushroom deleted");
                    return RedirectToPage("Index");
                }
                return Page();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }

            return Page();
        }
    }
}
