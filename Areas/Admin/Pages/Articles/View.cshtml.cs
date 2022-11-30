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
using MushroomWebsite.Repository.IRepository;

namespace MushroomWebsite.Areas.Admin.Pages.Articles
{
    public class ViewModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _db;
        readonly ILogger _log = Log.ForContext<ViewModel>();
        public IList<Mushroom> Mushrooms { get; set; }

        [BindProperty]
        public Entry Entry { get; set; }

        public ViewModel(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            Mushrooms = new List<Mushroom>();
        }

        public void OnGet(int id)
        {
            try
            {
                Entry = _unitOfWork.Entry.GetFirstOrDefault(u=>u.Id==id);
                Entry.User = _unitOfWork.User.GetFirstOrDefault(u=>u.Id==Entry.UserId);

                IQueryable<Article> queryArticle = _db.Set<Article>();
                Entry.Article = queryArticle.FirstOrDefault(u => u.EntryId == Entry.Id);

                if(Entry.Mushrooms != null)
                    Mushrooms = (IList<Mushroom>)Entry.Mushrooms;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            
        }

        public async Task<IActionResult> OnPost()
        {
            return Page();
        }
    }
}
