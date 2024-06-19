using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Data;
using MyRecipes.Models;
using Newtonsoft.Json;
using static System.Collections.Specialized.BitVector32;

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
        [HttpGet]
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
        
        public async Task<IActionResult> Details(int id)
        {
            var dishDetails = await _context.Dishes
             .Include(d => d.DishCategories)
             .ThenInclude(dc => dc.Category)
         .Include(d => d.Instructions)
         .Include(d => d.Recipes)
                 .ThenInclude(r => r.Ingredients)
         .Include(d => d.Recipes)
             .ThenInclude(r => r.Ingredients)
                 .ThenInclude(i => i.Unit)
         .FirstOrDefaultAsync(d => d.Id == id);
                if (dishDetails == null) return View("Empty");
                return View(dishDetails);
            }
        }
    } 

