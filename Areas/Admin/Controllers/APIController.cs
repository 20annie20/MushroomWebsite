using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MushroomWebsite.Data;
using MushroomWebsite.Models;
using MushroomWebsite.Repository.IRepository;

namespace MushroomWebsite.Areas.Admin.Controllers
{
    [Route("Admin/API/Mushrooms")]
    [ApiController]
    public class APIController : Controller
    {
        public IList<Mushroom> Mushrooms { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public APIController(ApplicationDbContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public DataTables.DataTableRequest request { get; set; }
        [HttpPost]

        public IActionResult Index()
        {
            Mushrooms = _unitOfWork.Mushroom.GetAll().ToList();

            var recordsTotal = Mushrooms.Count();

            var mushroomsQuery = Mushrooms.AsQueryable();

            var searchText = request.Search.Value?.ToUpper();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                mushroomsQuery = mushroomsQuery.Where(s =>
                    s.Name.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = mushroomsQuery.Count();

            var sortColumnName = request.Columns.ElementAt(request.Order.ElementAt(0).Column).Name;
            var sortDirection = request.Order.ElementAt(0).Dir.ToLower();

            // using System.Linq.Dynamic.Core
            mushroomsQuery = mushroomsQuery.OrderBy($"{sortColumnName} {sortDirection}");

            var skip = request.Start;
            var take = request.Length;
            var data = mushroomsQuery
                .Skip(skip)
                .Take(take)
                .ToList();

            return new JsonResult(new
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = data
            });
        }
    }
}
