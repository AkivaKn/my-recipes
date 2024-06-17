namespace MyRecipes.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
