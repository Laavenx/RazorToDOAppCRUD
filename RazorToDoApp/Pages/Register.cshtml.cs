using System.Net;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorToDoApp.Data;
using RazorToDoApp.Models;
using System.Numerics;

namespace RazorToDoApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterCredentials Credentials { get; set; }
        private readonly ApplicationDBContext _context;
        public RegisterModel(ApplicationDBContext context)
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

            var userExists = _context.User.Where(u => u.UserName == Credentials.UserName).Any();
            if (userExists) 
            {
                ViewData["UserExists"] = "User alread exists";
                return Page();
            }

            DbUser emptyUser = new();
            emptyUser.UserName = Credentials.UserName;
            emptyUser.Password = BCrypt.Net.BCrypt.HashPassword(Credentials.Password);
            _context.User.Add(emptyUser);
            await _context.SaveChangesAsync(); 

            return RedirectToPage("/Login");
        }
    }
}
