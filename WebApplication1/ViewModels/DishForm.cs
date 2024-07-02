using MyRecipes.Models;
using System.ComponentModel;

namespace MyRecipes.ViewModels
{
    public class DishForm
    {
        public int? Id { get; set; }
        [DisplayName("Title")]
        public string DishName { get; set; }
        public string? DishDescription { get; set; }
        public int? Servings { get; set; }
        public string? Notes { get; set; }
        public string? CategoriesJson { get; set; }
        public string? InstructionsJson { get; set; }
        public string? RecipesJson { get; set; }

        public List<DishCategory>? Categories =>
            string.IsNullOrEmpty(CategoriesJson) ? new List<DishCategory>() :
            Newtonsoft.Json.JsonConvert.DeserializeObject<List<DishCategory>>(CategoriesJson);

        public List<Instruction>? Instructions =>
            string.IsNullOrEmpty(InstructionsJson) ? new List<Instruction>() :
            Newtonsoft.Json.JsonConvert.DeserializeObject<List<Instruction>>(InstructionsJson);

        public List<Recipe>? Recipes =>
            string.IsNullOrEmpty(RecipesJson) ? new List<Recipe>() :
            Newtonsoft.Json.JsonConvert.DeserializeObject<List<Recipe>>(RecipesJson);
    }
}
