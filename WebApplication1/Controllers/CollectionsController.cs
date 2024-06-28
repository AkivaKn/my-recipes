using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Areas.Identity.Pages.Account;
using MyRecipes.Data;
using MyRecipes.Models;

namespace MyRecipes.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CollectionsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);

            var collections = await _context.Collection.Include(c => c.DishCollections).Where(c => c.UserId == currentUserId).ToListAsync();

            return View(collections);
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);

            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .Include(c => c.User)
                .Include(c => c.DishCollections)
                .ThenInclude(dc => dc.Dish)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }
            if (collection.UserId != currentUserId)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CollectionName")] Collection collection)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            collection.UserId = currentUserId;
            if (ModelState.IsValid)
            {
                _context.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.FindAsync(id);
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            if(currentUserId != collection.UserId)
            {
                return RedirectToAction(nameof(Index));
            }
            if (collection == null)
            {
                return NotFound();
            }
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CollectionName,UserId")] Collection collection)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);

            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && collection.UserId == currentUserId)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", collection.UserId);
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);

            var collection = await _context.Collection.FindAsync(id);
            if (collection != null && collection.UserId == currentUserId)
            {
                _context.Collection.Remove(collection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
            return _context.Collection.Any(e => e.Id == id);
        }
    }
}
