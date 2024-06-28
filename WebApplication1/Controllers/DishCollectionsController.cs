using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;
using MyRecipes.Models;

namespace MyRecipes.Controllers
{
    public class DishCollectionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DishCollectionsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

    
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,CollectionId")] DishCollection dishCollection)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var collection = await _context.Collection.FirstOrDefaultAsync(c => c.Id == dishCollection.CollectionId);
            if (collection != null && collection.UserId == currentUserId)
            {
                _context.Add(dishCollection);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AddToCollection", "Dishes",new {id = dishCollection.DishId});

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DishCollection dishCollection)
        {
            if (dishCollection == null) 
            {
                return NotFound();
            }
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var collection = await _context.Collection.FirstOrDefaultAsync(c => c.Id == dishCollection.CollectionId);
            if (collection != null && collection.UserId == currentUserId && dishCollection != null)
            {
                _context.DishCollection.Remove(dishCollection);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("AddToCollection", "Dishes", new { id = dishCollection.DishId });

        }

        private bool DishCollectionExists(int id)
        {
            return _context.DishCollection.Any(e => e.DishId == id);
        }
    }
}
