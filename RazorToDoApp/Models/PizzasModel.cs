namespace RazorToDoApp.Models
{
    public class PizzasModel
    {
        public string ImageTitle { get; set; }
        public string PizzaName { get; set; }   
        public float BasePrice { get; set; }   
        public bool Cheese { get; set; }
        public bool Mushroom { get; set; }
        public float FinalPrice { get; set; }
    }
}
