using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyRecipes.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Section Name")]
        public string RecipeName { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        [JsonIgnore]
        public Dish Dish { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
