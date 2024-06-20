using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyRecipes.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        [JsonIgnore]
        public Recipe Recipe { get; set; }
        [ValidateNever]
        public int? UnitId { get; set; }
        [ForeignKey("UnitId")]
        [JsonIgnore]

        public Unit Unit { get; set; }
        [ValidateNever]

        public double? Quantity { get; set; }

    }
}
