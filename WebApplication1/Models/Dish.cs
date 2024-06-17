
namespace MyRecipes.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public string DishDescription {  get; set; }
        public int Servings { get; set; }
        public string Notes { get; set; }
        public List<DishCategory> DishCategories { get; set; }
        public List<Instruction> Instructions { get; set; }
        public List<Recipe> Recipes { get; set; }

    }
}
