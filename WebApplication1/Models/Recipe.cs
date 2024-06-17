using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish Dish { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
