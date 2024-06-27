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
    public class CommentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Comments.Include(c => c.Dish).Include(c => c.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Dish)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "DishName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CommentBody,DishId,UserId")] Comment comment)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            comment.UserId = currentUserId;
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Dishes", new { id = comment.DishId });
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "DishName", comment.DishId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CommentBody,DishId,UserId")] Comment comment)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
        
            if (id != comment.Id)
            {
                return NotFound();
            }
            if (currentUserId != comment.UserId)
            {
                return RedirectToAction("Details", "Dishes", new { id = comment.DishId });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Dishes", new { id = comment.DishId });
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "DishName", comment.DishId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Dish)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            var comment = await _context.Comments.FindAsync(id);
            if (currentUserId != comment.UserId && !currentUserRoles.Contains("Admin"))
            {
                return RedirectToAction("Details", "Dishes", new { id = comment.DishId });
            }
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Dishes", new { id = comment.DishId });
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
