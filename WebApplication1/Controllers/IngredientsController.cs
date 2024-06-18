using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;

namespace MyRecipes.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly AppDbContext _context;
        public IngredientsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allIngredients = await _context.Ingredients.ToListAsync();

            return View(allIngredients);
        }
    }
}

