using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorToDoApp.Data;
using RazorToDoApp.Models;

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
        public void OnGet()
        {
            Response.Cookies.Append("MyCookie", "value1");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (Credentials.UserName == "12345" && Credentials.Password == "password")
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "12345"),
                    new Claim(ClaimTypes.Email, "Email@wp.pl"),
                    new Claim("UserType", "User")
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", principal);

                return RedirectToPage("Index");
            }
            //var something = context.Students.Add(emptyStudent);
            var cookieValue = Request.Cookies["MyCookie"];
            Response.Cookies.Append("MyCookie", "value1");
            //return RedirectToPage("Index");
            return Page();
        }
    }
}
