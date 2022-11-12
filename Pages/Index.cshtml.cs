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
    public class IndexModel : PageModel
    {

        public class LoginDTOUser
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string Password { get; set; }
        }


        private ILogger _log = Log.ForContext<RegisterModel>();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _hasher;

        public IndexModel(IUnitOfWork unitOfWork, IPasswordHasher<User> hasher)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
        }

        [BindProperty]
        public LoginDTOUser UserData { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(UserData.Name) || string.IsNullOrEmpty(UserData.Password))
            {
                ModelState.AddModelError("", "Pusta nazwa użytkownika lub hasło");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = _unitOfWork.User.GetFirstOrDefault(c => c.Name.Equals(UserData.Name));
                    if(newUser != null)
                    {
                        var result = _hasher.VerifyHashedPassword(newUser, newUser.PasswordHash, UserData.Password);

                        if (result == PasswordVerificationResult.Success)
                        {
                            if(newUser.RoleId == 1)
                            {
                                return RedirectToPage("/UserPanel", new { area = "Admin" });
                            }
                            else if (newUser.RoleId == 2)
                            {
                                return RedirectToPage("/UserPanel", new { area = "User" });
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Niepoprawna nazwa użytkownika lub hasło");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Niepoprawna nazwa użytkownika lub hasło");
                    }

                    
                }
            
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }

            return Page();
        }

        private string BuildMessage(string stringToSplit, int chunkSize)
        {
            var data = Enumerable.Range(0, stringToSplit.Length / chunkSize)
                .Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));

            string result = "The generated token is:";

            foreach (string str in data)
            {
                result += Environment.NewLine + str;
            }

            return result;
        }
    }
}
