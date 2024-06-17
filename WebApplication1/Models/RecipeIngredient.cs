using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        [ForeignKey("Id")]
        public Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey("Id")]
        public Ingredient Ingredient { get; set; }
        public int UnitId { get; set; }
        [ForeignKey("Id")]
        public Unit Unit { get; set; }
        public int Quantity { get; set; }
        public int IngredientNumber { get; set; }

    }
}
