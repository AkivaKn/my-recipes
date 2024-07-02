using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Models;
using MyRecipes.ViewModels;

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
            modelBuilder.Entity<DishCollection>().HasKey(dc => new
            {
                dc.DishId,
                dc.CollectionId,
            });
            modelBuilder.Entity<DishCollection>().HasOne(d => d.Dish).WithMany(dc => dc.DishCollections).HasForeignKey(d => d.DishId);
            modelBuilder.Entity<DishCollection>().HasOne(c => c.Collection).WithMany(dc => dc.DishCollections).HasForeignKey(d => d.CollectionId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<MyRecipes.Models.Collection> Collection { get; set; } = default!;
        public DbSet<MyRecipes.Models.DishCollection> DishCollection { get; set; } = default!;


    }
}
