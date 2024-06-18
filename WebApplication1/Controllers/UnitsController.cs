using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;

namespace MyRecipes.Controllers
{
    public class UnitsController : Controller
    {
        private readonly AppDbContext _context;
        public UnitsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allUnits = await _context.Units.ToListAsync();

            return View(allUnits);
        }
    }
}
