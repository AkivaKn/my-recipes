using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;
using MyRecipes.Models;
using Newtonsoft.Json;

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
        public async Task<IActionResult> CreatePost()
        {
            var currentUserId = _userManager.GetUserId(HttpContext.User);
            Console.WriteLine($"{ currentUserId} is the current users id");
            var dishName = Request.Form["DishName"];
            var dishDescription = Request.Form["DishDescription"];
            var servings = int.Parse(Request.Form["Servings"]);
            var notes = Request.Form["Notes"];
            var dish = new Dish
            {
                DishName = dishName,
                DishDescription = dishDescription,
                Servings = servings,
                Notes = notes,
                UserId = currentUserId
            };
            await _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
            var categoriesJson = Request.Form["categories"];
            var categories = JsonConvert.DeserializeObject<List<DishCategory>>(categoriesJson);
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    category.DishId = dish.Id;
                    await _context.DishCategories.AddAsync(category);
                }
                await _context.SaveChangesAsync();
            }
            var instructionsJson = Request.Form["Instructions"];
            var instructions = JsonConvert.DeserializeObject<List<Instruction>>(instructionsJson);
            if (instructions != null)
            {
                foreach (var instruction in instructions)
                {
                    instruction.DishId = dish.Id;
                    _context.Instructions.Add(instruction);
                }
            }
            await _context.SaveChangesAsync();

            var recipesJson = Request.Form["sections"];
            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(recipesJson);
            if (recipes != null)
            {
                foreach (var newRecipe in recipes)
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
            return View(dishDetails);
            }
           return View("Empty");
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
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
            var dishName = Request.Form["DishName"];
            var dishDescription = Request.Form["DishDescription"];
            var servings = int.Parse(Request.Form["Servings"]);
            var notes = Request.Form["Notes"];

            dishToUpdate.DishName = dishName;
            dishToUpdate.DishDescription = dishDescription;
            dishToUpdate.Servings = servings;
            dishToUpdate.Notes = notes;


            var categoriesJson = Request.Form["categories"];
            var categories = JsonConvert.DeserializeObject<List<DishCategory>>(categoriesJson);
            if (categories != null)
            {

                dishToUpdate.DishCategories.Clear();
                foreach (var category in categories)
                {
                    category.DishId = dishToUpdate.Id;
                    dishToUpdate.DishCategories.Add(category);
                }
            }


            var instructionsJson = Request.Form["instructions"];
            var instructions = JsonConvert.DeserializeObject<List<Instruction>>(instructionsJson);
            if (instructions != null)
            {

                dishToUpdate.Instructions.Clear();
                foreach (var instruction in instructions)
                {
                    instruction.DishId = dishToUpdate.Id;
                    dishToUpdate.Instructions.Add(instruction);
                }
            }


            var recipesJson = Request.Form["recipes"];
            var recipes = JsonConvert.DeserializeObject<List<Recipe>>(recipesJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            if (recipes != null)
            {

                dishToUpdate.Recipes.Clear();
                foreach (var newRecipe in recipes)
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

                                if (newIngredient.Unit.Id > 0)
                                {
                                    ingredient.UnitId = newIngredient.Unit.Id;
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
    }
}

