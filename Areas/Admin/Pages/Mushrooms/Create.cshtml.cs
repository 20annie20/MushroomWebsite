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


namespace MushroomWebsite.Areas.Admin.Pages.Mushrooms
{
    public class CreateModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        readonly ILogger _log = Log.ForContext<EditModel>();
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public Mushroom Mushroom { get; set; }

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile? file)
        {
            try
            {
                if(_unitOfWork.Mushroom.GetAll().Any(contact => contact.Name.Equals(Mushroom.Name)))
                {
                    ModelState.AddModelError("Mushroom.Name", "Taki grzyb już istnieje w bazie");
                }

                if(ModelState.IsValid)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    if(file != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"images");
                        var extension = Path.GetExtension(file.FileName);

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        };

                        Mushroom.ImageUrl = @"\images\" + fileName + extension;
                    }

                    _unitOfWork.Mushroom.Add(Mushroom);
                    _unitOfWork.Save();
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
