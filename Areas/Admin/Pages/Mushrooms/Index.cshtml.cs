using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MushroomWebsite.Data;
using MushroomWebsite.Models;
using MushroomWebsite.Repository.IRepository;

namespace MushroomWebsite.Areas.Admin.Pages.Mushrooms
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        public IList<Mushroom> Mushrooms { get; set; }

        public IndexModel(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
      
    }
}
