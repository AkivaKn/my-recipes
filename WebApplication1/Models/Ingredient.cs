using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
      
        public int? UnitId { get; set; }
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
        public double? Quantity { get; set; }

    }
}
