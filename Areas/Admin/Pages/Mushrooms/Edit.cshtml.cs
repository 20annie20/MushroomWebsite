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

namespace MushroomWebsite.Areas.Admin.Pages.Mushrooms
{
    public class EditModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        readonly ILogger _log = Log.ForContext<EditModel>();

        [BindProperty]
        public Mushroom Mushroom { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if(ModelState.IsValid)
                {
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
