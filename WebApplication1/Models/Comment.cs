using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [ValidateNever]
        public Dish Dish { get; set; }
        [ValidateNever]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}
