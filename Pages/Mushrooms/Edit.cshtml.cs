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
    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        readonly ILogger _log = Log.ForContext<EditModel>();

        [BindProperty]
        public Mushroom Mushroom { get; set; }

        public EditModel(ApplicationDbContext db)
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
                if(ModelState.IsValid)
                {
                    _db.Mushrooms.Update(Mushroom);
                    await _db.SaveChangesAsync();
                    _log.Information("Mushroom updated");
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
