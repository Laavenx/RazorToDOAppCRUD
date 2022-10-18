using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorToDoApp.Data;
using RazorToDoApp.Models;
using BCrypt.Net;

namespace RazorToDoApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginCredentials Credentials { get; set; }
        private readonly ApplicationDBContext _context;
        public LoginModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Tasks");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = _context.User.Where(u => u.UserName == Credentials.UserName).FirstOrDefault();
            if (user != null && BCrypt.Net.BCrypt.Verify(Credentials.Password, user.Password)) 
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("UserType", "User")
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", principal);

                return RedirectToPage("Index");
            }

            ViewData["credentialsValid"] = "Incorrect username or password";

            return Page();
        }
    }
}
