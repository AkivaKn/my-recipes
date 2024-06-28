using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRecipes.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Collection Name")]

        public string CollectionName { get; set; }
        [ValidateNever]
        public List<DishCollection> DishCollections { get; set; }
        [ValidateNever]
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}
