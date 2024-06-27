
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Recipe Name")]
        [Required(ErrorMessage = "Please provide a name")]
        public string DishName { get; set; }
        [Display(Name = "Description")]
        public string? DishDescription {  get; set; }
        [Display(Name = "Number of servings")]

        [ValidateNever]
        public int? Servings { get; set; }
        [Display(Name = "Recipe Notes")]

        [ValidateNever]
        public string? Notes { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public List<DishCategory> DishCategories { get; set; }
        public List<DishCollection> DishCollections { get; set; }

        [ValidateNever]
        public List<Instruction> Instructions { get; set; }
        [ValidateNever]
        public List<Comment> Comments { get; set; }
        [ValidateNever]

        public List<Recipe> Recipes { get; set; }

    }
}
