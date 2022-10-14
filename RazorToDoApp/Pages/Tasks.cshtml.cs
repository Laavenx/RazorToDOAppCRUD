using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorToDoApp.Data;

namespace RazorToDoApp.Pages
{
    [Authorize(Policy = "MustBeLoggedIn")]
    public class TasksModel : PageModel
    {
        private readonly ApplicationDBContext _context;
        public void OnGet()
        {
            string emailAdress = HttpContext.User.Identity.Name;
            System.Diagnostics.Debug.WriteLine(emailAdress);

        }
    }
}
