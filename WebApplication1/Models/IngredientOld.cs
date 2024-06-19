using System.ComponentModel.DataAnnotations;

namespace MyRecipes.Models
{
    public class IngredientOld
    {
        [Key]
        public int Id { get; set; }
        public string IngredientName { get; set; }
        //public List<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
