using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MushroomWebsite.Models;
using Serilog;
using MushroomWebsite.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace MushroomWebsite.Pages
{
    public class RegisterModel : PageModel
    {
        
        public class RegisterDTOUser
        {
            public string Email { get; set; }
            public string Name { get; set; }

            [Required]
            public string Password { get; set; }
        }

        readonly ILogger _log = Log.ForContext<RegisterModel>();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _hasher;

        public RegisterModel(IUnitOfWork unitOfWork, IPasswordHasher<User> hasher)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
        }

        [BindProperty]
        public RegisterDTOUser UserData { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (_unitOfWork.User.GetAll().Any(contact => contact.Name.Equals(UserData.Name)))
                {
                    ModelState.AddModelError("User.Name", "Taki Użytkownik już istnieje w bazie");
                }

                if (ModelState.IsValid)
                {
                    var newUser = new User()
                    {
                        Email = UserData.Email,
                        Name = UserData.Name,
                        RoleId = 2,
                    };

                    var hashed = _hasher.HashPassword(newUser, UserData.Password);
                    newUser.PasswordHash = hashed;

                    _unitOfWork.User.Add(newUser);
                    _unitOfWork.Save();
                    return RedirectToPage("/Index");
                }             
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }

            return Page();
        }
    }
}
