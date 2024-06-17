
using System.ComponentModel.DataAnnotations;

namespace MyRecipes.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<DishCategory> DishCategories { get; set; }

    }
}
