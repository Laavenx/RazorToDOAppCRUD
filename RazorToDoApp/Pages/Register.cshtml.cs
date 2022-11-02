using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorToDoApp.Models;
using RazorToDoApp.Entities;

namespace RazorToDoApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterCredentials Credentials { get; set; }
        private readonly Data.AppDbContext _context;
        public RegisterModel(Data.AppDbContext context)
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

            var userExists = _context.Users.Where(u => u.UserName == Credentials.UserName).Any();
            if (userExists) 
            {
                ViewData["UserExists"] = "User alread exists";
                return Page();
            }

            AppUser emptyUser = new();
            emptyUser.UserName = Credentials.UserName;
            emptyUser.Password = BCrypt.Net.BCrypt.HashPassword(Credentials.Password);
            _context.Users.Add(emptyUser);
            await _context.SaveChangesAsync(); 

            return RedirectToPage("/Login");
        }
    }
}
