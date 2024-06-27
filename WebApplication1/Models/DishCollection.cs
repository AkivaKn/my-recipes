using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class DishCollection
    {
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish Dish { get; set; }
        public int CollectionId { get; set; }
        [ForeignKey("CollectionId")]

        public Collection Collection { get; set; }
    }
}
