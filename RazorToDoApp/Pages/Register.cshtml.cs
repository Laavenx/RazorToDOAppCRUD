using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorToDoApp.Data;
using RazorToDoApp.Models;

namespace RazorToDoApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public LoginCredentials Credentials { get; set; }
        private readonly ApplicationDBContext _context;
        public RegisterModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyUser = new DbUser();
            emptyUser.UserName = "Admin";
            //emptyUser.Password = BCrypt.Net.BCrypt.HashPassword(Credentials.Password);
            // _context.User.Add(emptyUser);
            //await _context.SaveChangesAsync(); 
            return RedirectToPage("Index");
        }
    }
}
