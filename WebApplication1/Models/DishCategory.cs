using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class DishCategory
    {
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish Dish { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
