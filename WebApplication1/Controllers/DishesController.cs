using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;

namespace MyRecipes.Controllers
{
    public class DishesController : Controller
    {
        private readonly AppDbContext _context;
        public DishesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allDishes = await _context.Dishes
                 .Include(r => r.DishCategories)
            .ThenInclude(dc => dc.Category)
                .ToListAsync();
            
            return View(allDishes);
        }
    }
}
