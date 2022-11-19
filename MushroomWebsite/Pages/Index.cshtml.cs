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

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Http;

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
        private IConfiguration _config;

        public IndexModel(IUnitOfWork unitOfWork, IPasswordHasher<User> hasher, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
            _config = config;
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
                            var tokenString = BuildToken(newUser);

                            if (newUser.RoleId == 1)
                            {
                                HttpContext.Session.SetString("Token", tokenString);
                                return RedirectToPage("/UserPanel", new { area = "Admin" });
                            }
                            else if (newUser.RoleId == 2)
                            {
                                HttpContext.Session.SetString("Token", tokenString);
                                return RedirectToPage("/UserPanel", new { area = "User", Authorization = tokenString });
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

        private string BuildToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, _unitOfWork.Role.GetFirstOrDefault(c => c.Id.Equals(user.RoleId)).Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
