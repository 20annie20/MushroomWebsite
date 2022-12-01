using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MushroomWebsite.Models;
using MushroomWebsite.Data;
using Serilog;
using System.IO;
using System.Data;
using MushroomWebsite.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MushroomWebsite.Areas.Admin.Pages.Articles
{
    public class CreateModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        readonly ILogger _log = Log.ForContext<CreateModel>();
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Entry Entry { get; set; }

        public CreateModel(ApplicationDbContext db, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public int[] SelectedMushrooms { get; set; }

        public SelectList Mushrooms { get; set; }

        public void OnGet()
        {
            var MushroomsList = _db.Mushrooms.ToList<Mushroom>();
            Mushrooms = new SelectList(MushroomsList, nameof(Mushroom.Id), nameof(Mushroom.Name));
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    MushroomWebsite.Models.User UserToUpdate = _unitOfWork.User.GetFirstOrDefault(c => c.Name.Equals(userId));
                    
                    if(Entry.UserId == 0)
                    {
                        Entry.UserId = UserToUpdate.Id;
                        Entry.User = UserToUpdate;
                    }
                    if(UserToUpdate.Entries == null)
                    {
                        UserToUpdate.Entries = new List<Entry>();
                    }

                    foreach(int i in SelectedMushrooms)
                    {
                        var shroom = _db.Mushrooms.FirstOrDefault(c => c.Id == i);
                        var EntryMushroom = new EntryMushroom();
                        EntryMushroom.Mushroom = shroom;
                        EntryMushroom.Entry = Entry;
                        if(Entry.EntryMushrooms == null)
                        {
                            Entry.EntryMushrooms = new List<EntryMushroom>();
                        }
                        if (shroom.EntryMushrooms == null)
                        {
                            shroom.EntryMushrooms = new List<EntryMushroom>();
                        }
                        _db.EntryMushrooms.Add(EntryMushroom);
                    }
                    
                    UserToUpdate.Entries.Add(Entry);
                    Entry.Article.CreatedAt = DateTime.Now;
                    _unitOfWork.Entry.Add(Entry);
                    _unitOfWork.Save();
                    _log.Information("Entry added");
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
