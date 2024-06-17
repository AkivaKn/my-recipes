
namespace MyRecipes.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<DishCategory> DishCategories { get; set; }

    }
}
