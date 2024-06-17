namespace MyRecipes.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
