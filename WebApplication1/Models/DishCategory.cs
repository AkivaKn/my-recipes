using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class DishCategory
    {
        public int DishId { get; set; }
        [ForeignKey("Id")]
        public Dish Dish { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("Id")]
        public Category Category { get; set; }

    }
}
