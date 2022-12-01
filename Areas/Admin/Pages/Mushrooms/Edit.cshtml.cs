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
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace MushroomWebsite.Areas.Admin.Pages.Mushrooms
{
    public class EditModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        readonly ILogger _log = Log.ForContext<EditModel>();
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public Mushroom Mushroom { get; set; }

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public void OnGet(int id)
        {
            try
            {
                Mushroom = _unitOfWork.Mushroom.GetFirstOrDefault(u=>u.Id==id);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            
        }

        public async Task<IActionResult> OnPost(IFormFile? fileu)
        {
            try
            {
                if (_unitOfWork.Mushroom.GetAll().Any(contact => contact.Name.Equals(Mushroom.Name)))
                {
                    ;
                }


                if (ModelState.IsValid)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    if (fileu != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"images");
                        var extension = Path.GetExtension(fileu.FileName);

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            fileu.CopyTo(fileStreams);
                        };

                        Mushroom.ImageUrl = @"\images\" + fileName + extension;
                    }

                    _unitOfWork.Mushroom.Update(Mushroom);
                    _unitOfWork.Save();
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
