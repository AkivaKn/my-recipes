using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyRecipes.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        public string CollectionName { get; set; }
        [ValidateNever]
        public List<DishCollection> DishCollections { get; set; }
    }
}
