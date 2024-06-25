using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Models;

namespace MyRecipes.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishCategory>().HasKey(dc => new
            {
                dc.DishId,
                dc.CategoryId,
            });
            modelBuilder.Entity<DishCategory>().HasOne(d => d.Dish).WithMany(dc => dc.DishCategories).HasForeignKey(d => d.DishId);
            modelBuilder.Entity<DishCategory>().HasOne(c => c.Category).WithMany(dc => dc.DishCategories).HasForeignKey(d => d.CategoryId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<MyRecipes.Models.User> User { get; set; } = default!;


    }
}
