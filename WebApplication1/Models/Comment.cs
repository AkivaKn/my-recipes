using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public Dish Dish { get; set; }
    }
}
