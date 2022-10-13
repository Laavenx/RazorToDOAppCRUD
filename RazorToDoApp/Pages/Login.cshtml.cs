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
            var emptyUser = new DbUser();
            emptyUser.UserName = "Admin";
            emptyUser.Password = "Password";
            _context.User.Add(emptyUser);
            await _context.SaveChangesAsync();
            //var something = context.Students.Add(emptyStudent);
            var cookieValue = Request.Cookies["MyCookie"];
            Response.Cookies.Append("MyCookie", "value1");
            System.Diagnostics.Debug.WriteLine(Credentials.UserName);
            System.Diagnostics.Debug.WriteLine(cookieValue);
            return RedirectToPage("Index");
        }
    }
}
