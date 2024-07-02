using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;
using MyRecipes.Models;
using MyRecipes.ViewModels;
using Newtonsoft.Json;
using System.Text.Json;

namespace MyRecipes.Controllers
{
    [Authorize(Roles = "User,Manager,Admin")]

    public class DishesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DishesController(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<JsonResult> GetDishesJson()
        {
            var dishes = await _context.Dishes
                .Include(r => r.DishCategories)
                .ThenInclude(dc => dc.Category)
                .ToListAsync();
            var result = dishes.Select(d => new
            {
                d.Id,
                d.DishName,
                d.DishDescription,
                Categories = d.DishCategories.Select(dc => new
                {
                    dc.Category.Id,
                    dc.Category.CategoryName
                }).ToList()
            });

            return Json(result);
        }
       
        public async Task<IActionResult> Index()
        {
            var allCategories = await _context.Categories.ToListAsync();
            ViewBag.Categories = allCategories;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var units = await _context.Units.ToListAsync();
            ViewBag.Units = units;
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            return View();
        }
    

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost([Bind("DishName,DishDescription,Servings,Notes,CategoriesJson,InstructionsJson,RecipesJson")] DishForm dishForm)
        {
            if (!ModelState.IsValid)
            {
                var units = await _context.Units.ToListAsync();
                ViewBag.Units = units;
                var categories = await _context.Categories.ToListAsync();
                ViewBag.Categories = categories;
                return View(dishForm);
            }
           
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            Console.WriteLine($"{currentUserId} is the current users id");
            
            var dish = new Dish
            {
                DishName = dishForm.DishName,
                DishDescription = dishForm.DishDescription,
                Servings = dishForm.Servings,
                Notes = dishForm.Notes,
                UserId = currentUserId
            };
            await _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
            
            if (dishForm.Categories != null)
            {
                foreach (var category in dishForm.Categories)
                {
                    category.DishId = dish.Id;
                    await _context.DishCategories.AddAsync(category);
                }
                await _context.SaveChangesAsync();
            }
           
            if (dishForm.Instructions != null)
            {
                foreach (var instruction in dishForm.Instructions)
                {
                    instruction.DishId = dish.Id;
                    _context.Instructions.Add(instruction);
                }
            }
            await _context.SaveChangesAsync();

        
            if (dishForm.Recipes != null)
            {
                foreach (var newRecipe in dishForm.Recipes)
                {
                    var recipe = new Recipe
                    {
                        RecipeName = newRecipe.RecipeName,
                        DishId = dish.Id,
                    };
                    await _context.Recipes.AddAsync(recipe);
                    await _context.SaveChangesAsync();
                    foreach (var newIngredient in newRecipe.Ingredients)
                    {
                        var ingredient = new Ingredient
                        {
                            IngredientName = newIngredient.IngredientName,
                            RecipeId = recipe.Id,
                            UnitId = newIngredient.UnitId,
                            Quantity = newIngredient.Quantity,
                        };
                        await _context.Ingredients.AddAsync(ingredient);
                        await _context.SaveChangesAsync();
                    }
                }
            }


            return RedirectToAction("Details", "Dishes", new { id = dish.Id });
        }
     
        public async Task<IActionResult> Edit(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            var units = await _context.Units.ToListAsync();
            ViewBag.Units = units;
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
            var dishDetails = await _context.Dishes
             .Include(d => d.DishCategories)
             .ThenInclude(dc => dc.Category)
         .Include(d => d.Instructions.OrderBy(i => i.InstructionNumber))
         .Include(d => d.Recipes)
                 .ThenInclude(r => r.Ingredients.OrderBy(i => i.IngredientNumber))
         .Include(d => d.Recipes)
             .ThenInclude(r => r.Ingredients)
                 .ThenInclude(i => i.Unit)
                 .AsSplitQuery()
         .FirstOrDefaultAsync(d => d.Id == id);
            if (dishDetails != null && currentUserId == dishDetails.UserId || currentUserRoles.Contains("Admin"))
            {
                string categoriesJson = System.Text.Json.JsonSerializer.Serialize(dishDetails.DishCategories);
                string instructionsJson = System.Text.Json.JsonSerializer.Serialize(dishDetails.Instructions);
                string recipesJson = System.Text.Json.JsonSerializer.Serialize(dishDetails.Recipes.Select(r => new
                {
                    r.RecipeName,
                    Ingredients = r.Ingredients.Select(i => new
                    {
                        i.IngredientName,
                        i.Quantity,
                        Unit = i.Unit != null ? new { i.Unit.Id, i.Unit.UnitName } : null
                    })
                }));
                var dishForm = new DishForm
                {
                    Id = dishDetails.Id,
                    DishName = dishDetails.DishName,
                    DishDescription = dishDetails.DishDescription,
                    Servings = dishDetails.Servings,
                    Notes = dishDetails.Notes,
                    CategoriesJson = categoriesJson,
                    InstructionsJson = instructionsJson,
                    RecipesJson = recipesJson,
                };
                return View(dishForm);
            }
            return View("Empty");
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id, [Bind("Id,DishName,DishDescription,Servings,Notes,CategoriesJson,InstructionsJson,RecipesJson")] DishForm dishForm)
        {
            if (!ModelState.IsValid)
            {
                var units = await _context.Units.ToListAsync();
                ViewBag.Units = units;
                var categories = await _context.Categories.ToListAsync();
                ViewBag.Categories = categories;
                return View(dishForm);
            }
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            var dishToUpdate = await _context.Dishes
                .Include(d => d.DishCategories)
                .Include(d => d.Instructions)
                .Include(d => d.Recipes)
                    .ThenInclude(r => r.Ingredients)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dishToUpdate == null)
            {
                return NotFound();
            }
            if (currentUserId != dishToUpdate.UserId && !currentUserRoles.Contains("Admin"))
            {
                return RedirectToAction("Details", "Dishes", new { id = id });
            }
            

            dishToUpdate.DishName = dishForm.DishName;
            dishToUpdate.DishDescription = dishForm.DishDescription;
            dishToUpdate.Servings = dishForm.Servings;
            dishToUpdate.Notes = dishForm.Notes;

            if (dishForm.Categories != null)
            {

                dishToUpdate.DishCategories.Clear();
                foreach (var category in dishForm.Categories)
                {
                    category.DishId = dishToUpdate.Id;
                    dishToUpdate.DishCategories.Add(category);
                }
            }


           
            if (dishForm.Instructions != null)
            {

                dishToUpdate.Instructions.Clear();
                foreach (var instruction in dishForm.Instructions)
                {
                    instruction.DishId = dishToUpdate.Id;
                    dishToUpdate.Instructions.Add(instruction);
                }
            }


            
            if (dishForm.Recipes != null)
            {

                dishToUpdate.Recipes.Clear();
                foreach (var newRecipe in dishForm.Recipes)
                {
                    if (newRecipe.RecipeName != null)
                    {
                        var recipe = new Recipe
                        {
                            RecipeName = newRecipe.RecipeName,
                            DishId = dishToUpdate.Id,
                        };
                        _context.Recipes.Add(recipe);
                        await _context.SaveChangesAsync();
                        foreach (var newIngredient in newRecipe.Ingredients)
                        {
                            if (newIngredient.IngredientName != null)
                            {
                                var ingredient = new Ingredient
                                {
                                    IngredientName = newIngredient.IngredientName,
                                    RecipeId = recipe.Id,
                                    IngredientNumber = newIngredient.IngredientNumber,
                                };

                                if (newIngredient.UnitId != null)
                                {
                                    ingredient.UnitId = newIngredient.UnitId;
                                }

                                if (newIngredient.Quantity > 0)
                                {
                                    ingredient.Quantity = newIngredient.Quantity;
                                }
                                _context.Ingredients.Add(ingredient);
                            }
                        }
                    }

                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Dishes", new { id = id });
        }
        public async Task<IActionResult> Details(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            ViewBag.currentUserId = currentUserId;
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            var collections = await _context.Collection.Where(c => c.UserId == currentUserId).ToListAsync();
            ViewBag.collections = collections;
            var dishDetails = await _context.Dishes
                .Include(d => d.User)
             .Include(d => d.DishCategories)
             .ThenInclude(dc => dc.Category)
         .Include(d => d.Instructions.OrderBy(i => i.InstructionNumber))
         .Include(d => d.Recipes)
                 .ThenInclude(r => r.Ingredients)
         .Include(d => d.Recipes)
             .ThenInclude(r => r.Ingredients.OrderBy(i => i.IngredientNumber))
                 .ThenInclude(i => i.Unit)
                 .Include(d=> d.Comments)
                 .Include(d => d.DishCollections)
                 .ThenInclude(dc => dc.Collection)
                 .AsSplitQuery()
         .FirstOrDefaultAsync(d => d.Id == id);
            
            if (dishDetails == null) return View("Empty");
            return View(dishDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
            if (id == null)
            {
                return NotFound();
            }
            var dish = await _context.Dishes.FindAsync(id);
            if (dish != null && currentUserId == dish.UserId || currentUserRoles.Contains("Admin"))
            {
                _context.Dishes.Remove(dish);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddToCollection(int id)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            ViewBag.DishId = id;
            var collections = await _context.Collection.Include(c=> c.DishCollections).Where(c => c.UserId == currentUserId).ToListAsync();
            return View(collections);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCollection([Bind("DishId,CollectionId")] DishCollection dishCollection)
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            var collection = await _context.Collection.FirstOrDefaultAsync(c => c.Id == dishCollection.CollectionId);
            if (collection != null && collection.UserId == currentUserId)
            {
                _context.Add(dishCollection);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AddToCollection", "Dishes", new { id = dishCollection.DishId });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFromCollection(DishCollection dishCollection)
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
    }
}

