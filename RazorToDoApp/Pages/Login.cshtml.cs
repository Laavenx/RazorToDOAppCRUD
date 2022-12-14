using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorToDoApp.Data;
using RazorToDoApp.Models;

namespace RazorToDoApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginCredentials Credentials { get; set; }
        private readonly AppDbContext _context;
        public LoginModel(AppDbContext context)
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

            var user = _context.Users.Where(u => u.UserName == Credentials.UserName).FirstOrDefault();
            if (user != null && BCrypt.Net.BCrypt.Verify(Credentials.Password, user.Password)) 
            {
                var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("UserType", "User")
                }, "MyCookieAuth"));
                
                await HttpContext.SignInAsync("MyCookieAuth", claims);

                return RedirectToPage("/Tasks");
            }

            ViewData["credentialsValid"] = "Incorrect username or password";

            return Page();
        }
    }
}
