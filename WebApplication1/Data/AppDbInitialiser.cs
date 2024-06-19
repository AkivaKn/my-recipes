using MyRecipes.Models;

namespace MyRecipes.Data
{
    public class AppDbInitialiser
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if (!context.Dishes.Any())
                {
                    context.Dishes.AddRange(new List<Dish>()
                    { new Dish()
                        {
                            DishName ="Baked Blintz Casserole",
                        },
                        new Dish()
                        {
                            DishName ="Layer Cake (Mishpacha)",
                            Servings = 36,
                            Notes = "For best results freeze before slicing."
                        },
                       });
                    context.SaveChanges();
                }
                Console.WriteLine($"Dishes count after seeding: {context.Dishes.Count()}");
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            CategoryName = "Dairy"
                        },
                        new Category()
                        {
                            CategoryName = "Casseroles"
                        },
                        new Category()
                        {
                            CategoryName = "Cakes"
                        },
                        });
                    context.SaveChanges();
                }
                if (!context.DishCategories.Any())
                {
                    context.DishCategories.AddRange(new List<DishCategory>()
                    {
                        new DishCategory()
                        {
                            DishId = 1,
                            CategoryId = 1,
                        },
                        new DishCategory()
                        {
                            DishId = 1,
                            CategoryId = 2,
                        },
                        new DishCategory()
                        {
                            DishId = 2,
                            CategoryId = 3,
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Instructions.Any())
                {
                    context.Instructions.AddRange(new List<Instruction>()
                    {
                        new Instruction()
                        {
                            InstructionNumber = 1,
                            InstructionBody = "Preheat oven to 160. Butter a 9 by 13 inch tin or coat it with non stick spray.",
                            DishId = 1,
                        },
                        new Instruction()
                        {
                            InstructionNumber = 2,
                            InstructionBody = "Blend all batter ingredients until very smooth.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 3,
                            InstructionBody = "Take 1.5 cups of the batter and pour in tin. Bake for about 10 minu or so until set.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 4,
                            InstructionBody = "Mix well all filling ingredients. Spread the filling over the baked batter, smoothing the top. Give the remaining batter a brief stir to re-suspend the ingredients, then very slowly pour it over the cheese filling so the filling is completely covered.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 5,
                            InstructionBody = "Carefully return the casserole to the oven and bake an additional 35-40 min or until the top is puffed and set.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 6,
                            InstructionBody = "Garnish with sour cream, yoghurt, applesauce and/or fruit.",
                            DishId = 1,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 1,
                            InstructionBody = "Combine all shortbread ingredients and mix. Refrigerate until needed.",
                            DishId = 2,
                        },
                        new Instruction()

                        {
                            InstructionNumber = 2,
                            InstructionBody = "Over very low flame or double boiler, melt chocolate, sugar, cocoa and water stirring occasionally. Remove from heat and stir in the margarine. Allow to cool.",
                            DishId = 2,
                        },
                         new Instruction()

                        {
                            InstructionNumber = 3,
                            InstructionBody = "Beat the egg whites until stiff. Slowly add in all the yolks, then the flour and baking powder. Fold in the chocolate mixture and mix by hand.",
                            DishId = 2,
                        },
                          new Instruction()

                        {
                            InstructionNumber = 4,
                            InstructionBody = "Roll out the shortbread dough to fit into a baking sheet. Top with fudge cake mixture. Bake at 350 for 50-60 minutes, until baked through.",
                            DishId = 2,
                        },
                           new Instruction()

                        {
                            InstructionNumber = 5,
                            InstructionBody = "Roll out a sheet of flaky dough to fit a cookie sheet, and pierce with a fork. Bake at 350 until golden.",
                            DishId = 2,
                        },
                            new Instruction()

                        {
                            InstructionNumber = 6,
                            InstructionBody = "Pipe the custard on top of the cooled cake. Place the baked flaky dough on top. Cut the cake to the desired size. Sprinkle with confectioner's sugar.",
                            DishId = 2,
                        },

                    });
                    context.SaveChanges();

                }
                if (!context.Recipes.Any())
                {
                    context.Recipes.AddRange(new List<Recipe>()
                    {
                        new Recipe()
                        {
                            RecipeName = "Batter",
                            DishId = 1,
                        },
                         new Recipe()
                        {
                            RecipeName = "Filling",
                            DishId = 1,
                        },
                          new Recipe()
                        {
                            RecipeName = "For Serving",
                            DishId = 1,
                        },
                           new Recipe()
                        {
                            RecipeName = "Shortbread Layer",
                            DishId = 2,
                        },
                            new Recipe()
                        {
                            RecipeName = "Fudge Cake Layer",
                            DishId = 2,
                        },
                    });
                    context.SaveChanges();

                }
                if (!context.Units.Any())
                {
                    context.Units.AddRange(new List<Unit>()
                    {
                        new Unit()
                        {
                            UnitName = "cup",
                            PluralName = "cups"
                        },
                        new Unit()
                        {
                            UnitName = "Tbsp",
                            PluralName = "Tbsp"
                        },
                        new Unit()
                        {
                            UnitName = "tsp",
                            PluralName = "tsp"
                        },
                        new Unit()
                        {
                            UnitName = "gr",
                            PluralName = "gr"
                        },
                        new Unit()
                        {
                            UnitName = "stick",
                            PluralName = "sticks"
                        },
                        new Unit()
                        {
                            UnitName = "oz",
                            PluralName = "oz"
                        },
                        new Unit()
                        {
                            UnitName = "sheet",
                            PluralName = "sheets"
                        },
                         new Unit()
                        {
                            UnitName = "kg",
                            PluralName = "kg"
                        },
                          new Unit()
                        {
                            UnitName = "ml",
                            PluralName = "ml"
                        },
                           new Unit()
                        {
                            UnitName = "l",
                            PluralName = "l"
                        },
                            new Unit()
                        {
                            UnitName = "pinch",
                            PluralName = "pinch"
                        },
                    });
                    context.SaveChanges();
                }
                if (!context.Ingredients.Any())
                {
                    context.Ingredients.AddRange(new List<Ingredient>()
                    {
                        new Ingredient()
                        {
                            IngredientName = "large egg",
                            RecipeId = 1,
                            Quantity = 4
                        },
                        new Ingredient()
                        {
                            IngredientName = "milk",
                             RecipeId = 1,
                            UnitId = 1,
                            Quantity = 1.25,
                        },
                        new Ingredient()
                        {
                            IngredientName = "sour cream",
                            RecipeId = 1,
                            UnitId = 2,
                            Quantity = 2,
                        },
                        new Ingredient()
                        {
                            IngredientName = "melted butter",
                            RecipeId = 1,
                            UnitId = 1,
                            Quantity = 0.25,
                        },
                        new Ingredient()
                        {
                            IngredientName = "vanilla extract",
                            RecipeId = 1,
                            UnitId = 3,
                            Quantity = 0.75,
                        },
                        new Ingredient()
                        {
                            IngredientName = "flour",
                            RecipeId = 1,
                            UnitId = 1,
                            Quantity = 1.33,
                        },
                        new Ingredient()
                        {
                            IngredientName = "sugar",
                            RecipeId = 1,
                            UnitId = 2,
                            Quantity = 1.5,
                        },
                        new Ingredient()
                        {
                            IngredientName = "baking powder",
                            RecipeId = 1,
                            UnitId = 3,
                            Quantity = 1.25,
                        },
                        new Ingredient()
                        {
                            IngredientName = "curd style farmer cheese",
                            RecipeId = 2,
                            UnitId = 4,
                            Quantity = 225,
                        },
                        new Ingredient()
                        {
                            IngredientName = "ricotta cheese",
                            RecipeId = 2,
                            UnitId = 4,
                            Quantity = 450,
                        },
                        new Ingredient()
                        {
                            IngredientName = "large egg",
                             RecipeId = 2,
                            Quantity = 2,
                        },
                         new Ingredient()
                        {
                            IngredientName = "sugar",
                            RecipeId = 2,
                            UnitId = 2,
                            Quantity = 2.5,
                        },
                        new Ingredient()
                        {
                            IngredientName = "lemon juice",
                            RecipeId = 2,
                            UnitId = 2,
                            Quantity = 2,
                        },
                         new Ingredient()
                        {
                            IngredientName = "sour cream",
                            RecipeId = 3,
                        },
                        new Ingredient()
                        {
                            IngredientName = "plain/vanilla yoghurt",
                            RecipeId = 3,
                        },
                        new Ingredient()
                        {
                            IngredientName = "applesauce",
                            RecipeId = 3,
                        },
                        new Ingredient()
                        {
                            IngredientName = "sliced fresh strawberries or other fruit",
                            RecipeId = 3,
                        },
                        new Ingredient()
                        {
                            IngredientName = "flour",
                            RecipeId = 4,
                            UnitId = 1,
                            Quantity = 3,
                        },
                        new Ingredient()
                        {
                            IngredientName = "baking powder",
                            RecipeId = 4,
                            UnitId = 3,
                            Quantity = 1,
                        },
                        new Ingredient()
                        {
                            IngredientName = "margarine",
                            RecipeId = 4,
                            UnitId = 5,
                            Quantity = 3,
                        },
                        new Ingredient()
                        {
                            IngredientName = "egg yolks",
                             RecipeId = 4,
                            Quantity = 2,
                        },
                        new Ingredient()
                        {
                            IngredientName = "water",
                            RecipeId = 4,
                            UnitId = 1,
                            Quantity = 0.33,
                        },
                        new Ingredient()
                        {
                            IngredientName = "sugar",
                             RecipeId = 4,
                            UnitId = 2,
                            Quantity = 2,
                        },
                        new Ingredient()
                        {
                            IngredientName = "salt",
                             RecipeId = 4,
                            UnitId = 3,
                            Quantity = 0.5,
                        },
                        new Ingredient()
                        {
                            IngredientName = "chocolate",
                              RecipeId = 5,
                            UnitId = 6,
                            Quantity = 8,
                        },
                        new Ingredient()
                        {
                            IngredientName = "sugar",
                             RecipeId = 5,
                            UnitId = 1,
                            Quantity = 2,
                        },
                        new Ingredient()
                        {
                            IngredientName = "cocoa",
                             RecipeId = 5,
                            UnitId = 2,
                            Quantity = 2,
                        },
                         new Ingredient()
                        {
                            IngredientName = "water",
                           RecipeId = 5,
                            UnitId = 1,
                            Quantity = 0.5,
                        },
                          new Ingredient()
                        {
                            IngredientName = "margarine",
                             RecipeId = 5,
                            UnitId = 5,
                            Quantity = 1,
                        },
                        new Ingredient()
                        {
                            IngredientName = "separated egg",
                              RecipeId = 5,
                            Quantity = 7,
                        },
                         new Ingredient()
                        {
                            IngredientName = "egg yolks",
                               RecipeId = 5,
                            Quantity = 5,
                        },
                          new Ingredient()
                        {
                            IngredientName = "flour",
                             RecipeId = 5,
                            UnitId = 2,
                            Quantity = 5,
                        },
                        new Ingredient()
                        {
                            IngredientName = "baking powder",
                             RecipeId = 5,
                            UnitId = 3,
                            Quantity = 1.5,
                        },
                        new Ingredient()
                        {
                            IngredientName = "bakers choice custard",
                             RecipeId = 5,
                            UnitId = 6,
                            Quantity = 12,
                        },
                        new Ingredient()
                        {
                            IngredientName = "flaky dough",
                             RecipeId = 5,
                            UnitId = 7,
                            Quantity = 2,
                        },
                        new Ingredient()
                        {
                            IngredientName = "confectioner's sugar",
                             RecipeId = 5,
                        }

                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
