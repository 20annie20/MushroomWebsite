﻿using System;
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
        readonly ILogger _log = Log.ForContext<EditModel>();

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
                if(_db.Mushrooms.Any(contact => contact.Name.Equals(Mushroom.Name)))
                {
                    ModelState.AddModelError("Mushroom.Name", "Taki grzyb już istnieje w bazie");
                }

                if(ModelState.IsValid)
                {
                    await _db.Mushrooms.AddAsync(Mushroom);
                    await _db.SaveChangesAsync();
                    _log.Information("Mushroom added");
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
