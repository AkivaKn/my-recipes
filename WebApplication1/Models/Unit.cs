using System.ComponentModel.DataAnnotations;

namespace MyRecipes.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string UnitName { get; set; }
        public string PluralName { get; set; }
        public List<Ingredient> Ingredients { get; set; }

    }
}
