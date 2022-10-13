using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorToDoApp.Models;

namespace RazorToDoApp.Pages
{
    public class PizzaModel : PageModel
    {
        public List<PizzasModel> fakePizzaDB = new List<PizzasModel>
        {
            new PizzasModel(){ImageTitle="Margerita",
                PizzaName="Margerita",
                BasePrice=2,
                Cheese=true,
                FinalPrice=4},
            new PizzasModel(){ImageTitle="Mushroom",
                PizzaName="Mushroom",
                BasePrice=3,
                Mushroom=true,
                FinalPrice=5},
        };
        public void OnGet()
        {
        }
    }
}
