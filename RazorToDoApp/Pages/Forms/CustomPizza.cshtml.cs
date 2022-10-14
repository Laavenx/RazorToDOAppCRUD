using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorToDoApp.Models;

namespace RazorToDoApp.Pages.Forms
{
    public class CustomPizzaModel : PageModel
    {
        [BindProperty]
        //public PizzasModel Pizza { get; set; }
        public float PizzaPrice { get; set; }
        public void OnGet()
        {

        }

    }
}
