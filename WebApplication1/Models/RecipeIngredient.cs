using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredient Ingredient { get; set; }
        public int? UnitId { get; set; }
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
        public double? Quantity { get; set; }

    }
}
