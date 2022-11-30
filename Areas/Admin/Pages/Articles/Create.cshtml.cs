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


namespace MushroomWebsite.Areas.Admin.Pages.Articles
{
    public class CreateModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        readonly ILogger _log = Log.ForContext<CreateModel>();
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public Entry Entry { get; set; }
        public MushroomWebsite.Models.User Author{ get; set; }

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Entry.UserId = Author.Id;
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
