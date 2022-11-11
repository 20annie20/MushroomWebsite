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

namespace MushroomWebsite.Areas.User.Pages.Mushrooms
{
    public class CreateModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        readonly ILogger _log = Log.ForContext<CreateModel>();

        [BindProperty]
        public Mushroom Mushroom { get; set; }

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if(_unitOfWork.Mushroom.GetAll().Any(contact => contact.Name.Equals(Mushroom.Name)))
                {
                    ModelState.AddModelError("Mushroom.Name", "Taki grzyb już istnieje w bazie");
                }

                if(ModelState.IsValid)
                {
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
