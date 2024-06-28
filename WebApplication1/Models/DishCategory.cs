using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyRecipes.Models
{
    public class DishCategory
    {
        [Key] 
        public int Id { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        [JsonIgnore]
        public Dish Dish { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [JsonIgnore]

        public Category Category { get; set; }

    }
}
