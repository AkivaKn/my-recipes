using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class DishCollection
    {
        [Key]
        public int Id { get; set; }
        public int DishId { get; set; }
        [ForeignKey("DishId")]
        [ValidateNever]
        public Dish Dish { get; set; }
        public int CollectionId { get; set; }
        [ForeignKey("CollectionId")]
        [ValidateNever]
        public Collection Collection { get; set; }
    }
}
